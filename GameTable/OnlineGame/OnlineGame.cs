using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTable.NamespaceGame;
using GameTable.CardDeck;
using System.Xml.Serialization;
using System.IO;
using System.Net.Sockets;
using System.Net;
using GameTable.NamespacePlayers;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace GameTable.OnlineGame
{
    [Serializable]
    public class SendingData
    {
        public string messageCommand { get; set; }
        public ShortCard card { get; set; }
        public int account { get; set; }
        public string name { get; set; }
        public int ToUser { get; set; }
        public List<string> scoreTableSend { get; set; }
        public List<string> AllUsers { get; set; }
        public SendingData()
        { }
    }

    public class OnlineGame:BaseGame
    {
        XmlSerializer _sendDetailsSerializer = new XmlSerializer(typeof(SendingData));
        MemoryStream _stream = new MemoryStream();

        int remotePort;
        int myPort;
        UdpClient sender = new UdpClient();
        IPEndPoint endPoint;
        IPAddress remoteIPAddress;
        byte[] _bufer;
        string _userName;
        int counter = 0;

        HumanPlayer myPlayer;

        /// <summary>
        /// Попытка подключения к серверу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="myPort"></param>
        /// <param name="remotePort"></param>
        /// <param name="remoteIpAddress"></param>
        public void ConnectToServer(string name, string myPort, string remotePort, string remoteIpAddress)
        {
            this.myPort = Convert.ToInt32(myPort);
            this.remotePort = Convert.ToInt32(remotePort);
            sender = new UdpClient(this.myPort);
            this.remoteIPAddress = IPAddress.Parse(remoteIpAddress);
            endPoint = new IPEndPoint(this.remoteIPAddress, this.remotePort);
            myPlayer = new HumanPlayer(name);

            DelegatesData.HandlerPlayerIsMoreThanEnough =
                new DelegatesData.PlayerIsMoreThanEnough(TurnComesToNextPlayer);

            try
            {
                sender.Connect(endPoint); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Thread thread = new Thread(Listener);
            thread.IsBackground = true;
            thread.Start();
        }

        public void ComeToTable()
        {
            SendMessage("onTable@");
        }
        private void SendMessage(string message)
        {
            MemoryStream _stream = new MemoryStream();
            _sendDetailsSerializer.Serialize(_stream, new SendingData() {messageCommand = message });
            _stream.Position = 0;
            _bufer = new byte[_stream.Length];
            _stream.Read(_bufer, 0, Convert.ToInt32(_stream.Length));

            try
            {
                sender.Send(_bufer, _bufer.Length);
                _stream.Close();
            }
            catch (SocketException se)
            {
                if (se.ErrorCode != 1004) MessageBox.Show(se.Message);
            }
            catch (Exception exs)
            {
                MessageBox.Show(exs.Message);
            }
        }

        private void Listener()
        {
            

            SendingData _send = new SendingData();
            List<string> users = new List<string>();
            IPEndPoint RemoteIPEndPoint = null;
            SendMessage("newPlayer@"+myPlayer.playersName);

            try
            {
                while (true)
                {
                   _bufer = sender.Receive(ref RemoteIPEndPoint);
                    //_bufer = _sender.Receive(ref _endPoint);
                    MemoryStream _stream = new MemoryStream();
                    _stream.Write(_bufer, 0, _bufer.Length);
                    _stream.Position = 0;

                   // MessageBox.Show(_bufer.Length + " " + _stream.Length);

                    _send = (SendingData)_sendDetailsSerializer.Deserialize(_stream);
                    string[] detailedMessageCommand = _send.messageCommand.Split('@');
                    
                    switch (detailedMessageCommand[0])
                    {
                        case "replace":
                            DelegatesData.HandlerPlayersListRefresh(_send);

                            break;
                        case "startGame":
                            DelegatesData.HandlerGameTableOpen();
                            break;

                        case "newCard":
                            PlayerRecieveStartCards(_send);
                            break;
                        case "playersTurn":
                            DelegatesData.HandlerTableButtonsIsEnanbleChange(true);
                            break;

                        case "winner":
                            AnnouncementOfWinners(detailedMessageCommand[1], _send.scoreTableSend);
                            break;
                        case "restart":
                            GameRestart();
                            break;
                        

                        default:break;
                    }
                }

            }
            catch (SocketException se)
            {
                if(se.ErrorCode != 1004)
                {
                    MessageBox.Show(se.Message);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
                    
      

        public override HumanPlayer GetCurrentHumanPlayer()
        { return myPlayer; }

        public override void HumanPlayerGetsCard()
        {
            SendMessage("hit@");
            
        }

        public override void HumanStopPlaying()
        {
            DelegatesData.HandlerTableButtonsIsEnanbleChange(false);
            SendMessage("stop@"+myPlayer.playersName+"@"+myPlayer.GetPlayersPoints().ToString());
        }

        public override void TurnComesToNextPlayer()
        { 
            HumanStopPlaying();
        }

        public void AnnouncementOfWinners(string s, List<string> sl)
        {
            string winner="";

            foreach (string str in sl)
            {
                winner += str;
            }
            winner+=s;
            
            DelegatesData.HandlerGameTableStatisticTB(winner);
        }
      
        public override void AnnouncementOfWinners() { } 
        

        public override void GameStart(int CompPlayersQty)
        {
            ComeToTable();
        }
        public void CloseServer()
        {
            try
            {
                 SendMessage("disconnect@");
                 sender.Close();

            }
            catch (SocketException se)
            {
                if (se.ErrorCode != 1004)
                {
                    MessageBox.Show(se.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void PlayerRecieveStartCards(SendingData s)
        {
                PlayerAcceptCard(s);
        }

        void PlayerAcceptCard(SendingData s)
        {
            if (s.card.GetHierarchy() == CardHierarchy.Ace)
            {
                TwoPointCard tc = new TwoPointCard(s.card.GetHierarchy(), s.card.GetSuit());
                myPlayer.cardsOnHand.AddCardToDeck(tc);
            }
            else
            {
                OnePointCard oc = new OnePointCard(s.card.GetHierarchy(), s.card.GetSuit());
                myPlayer.cardsOnHand.AddCardToDeck(oc);
            }

            if (myPlayer.GetPlayersPoints() >= 21)
            {
                HumanStopPlaying();
            }

            PlayersPoolCreate();
        }
        public override void CreateNewDeck()
        {
            throw new NotImplementedException();
        }

        public override void GameRestart()
        {
            myPlayer.cardsOnHand.NullifyDeck();
            DelegatesData.HandlerGameTableStatisticTB("");
            DelegatesData.HandlerCreateTableViewForCurrentPlayer();
        }
    }
}
