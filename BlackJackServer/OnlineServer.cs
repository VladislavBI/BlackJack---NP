
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using BlackJackServer.CardDeck;
using System.IO;
using System.Windows.Threading;

namespace BlackJackServer
{
    /// <summary>
    /// Текущий статус сервера
    /// </summary>
    public enum ServerStatus { Started, GameInProgress, Stopped };
    #region подключенные игроки
    /// <summary>
    /// Хранение инфы о подключенных игроках
    /// </summary>
    public class ConnectedUser
    {


        public ConnectedUser(string name, IPEndPoint ip)
        {
            userName = name;
            userIpEndPoint = ip;
        }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// Локальный адрес пользователя
        /// </summary>
        public IPEndPoint userIpEndPoint { get; set; }
    }
    /// <summary>
    /// Класс для сравнения портов игроков
    /// </summary>
    public class ConnectedUserComparer : IEqualityComparer<ConnectedUser>
    {

        bool IEqualityComparer<ConnectedUser>.Equals(ConnectedUser x, ConnectedUser y)
        {
            return x.userIpEndPoint == y.userIpEndPoint;
        }

        int IEqualityComparer<ConnectedUser>.GetHashCode(ConnectedUser obj)
        {
            int hCode = Convert.ToInt32(obj.userName) ^ obj.userIpEndPoint.Port;
            return hCode.GetHashCode();
        }
    }
    #endregion


    [Serializable]
    public class SendingData
    {
        public string messageCommand { get; set; }
        public ShortCard card { get; set; }
        public int account { get; set; }
        public string name { get; set; }
        public int ToUser { get; set; }
        public List<string> scoreTableSend { get; set;}
        public List<string> AllUsers { get; set; }
        public SendingData()
        {}
    }
    public partial class MainWindow
    {
        int localPort;//
        UdpClient server;//
        IPEndPoint remoteEndPoint;//
        /// <summary>
        /// имя текущего подключенного игрока
        /// </summary>
        string userName;//
        string messageCommand;//
        byte[] buffer;//
        List<ConnectedUser> allUsers = new List<ConnectedUser>();
        UnicodeEncoding encodingMessage = new UnicodeEncoding();
        XmlSerializer dataSerializer = new XmlSerializer(typeof(SendingData));

        Dictionary<string, int> scoresTable = new Dictionary<string, int>();

        /// <summary>
        /// Колода сервера
        /// </summary>
        GeneralDeck serverDeck;
        
        /// <summary>
        /// Текущее состояние сервера
        /// </summary>
        ServerStatus sStatus = ServerStatus.Stopped;

        #region Открытие сервера
        /// <summary>
        /// Создание нового сервера
        /// </summary>
        public void InitializeServer()
        {
            StartUdpClient();
            StartListener();
        }
        /// <summary>
        /// Создает UDP Сервер
        /// </summary>
        private void StartUdpClient()
        {
            try
            {
                localPort = 7777;
                server = new UdpClient(localPort);
                remoteEndPoint = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Начинает прослушку входящих сообщений
        /// </summary>
        private void StartListener()
        {
            Thread thread = new Thread(Listener);
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion

        public void NewServerDeckCreate()
        {
            serverDeck = new GeneralDeck();
        }
        /// <summary>
        /// Начало игры
        /// </summary>
        void StartGame()
        {
            
            foreach (var item in allUsers)
            {
                SendGameStart(item.userIpEndPoint);
            }
            GetStartCards();
            try
            {
                SendYouPlay(allUsers[0].userIpEndPoint);
            }
            catch (Exception)
            {
                MessageBox.Show("Нет активных игроков!");
                StatusChangeEffect(ServerStatus.Stopped);

            }
           
        }

        /// <summary>
        /// Раздача стартовой колоды
        /// </summary>
        void GetStartCards()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var item in allUsers)
                {
                    SendCard(item.userIpEndPoint, serverDeck.GetShortCard());
                }  
            } 
        }
        #region Сообщения игроку
        /// <summary>
        /// Сообщение о начале игры
        /// </summary>
        /// <param name="endP"></param>
        void SendGameStart(IPEndPoint endP)
        {
            UdpClient ucl = new UdpClient();
            MemoryStream memory = new MemoryStream();

            dataSerializer.Serialize(memory, new SendingData() { messageCommand = "strartGame@"});
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, Convert.ToInt32(memory.Length));

            try
            {
                server.Send(buffer, buffer.Length, endP);
                //ucl.Send(buffer, buffer.Length, endP);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Отправка списка игроков
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="endP">Адресс пользователя</param>
        /// <param name="CARD"></param>
        private void SendNamesList(string message, IPEndPoint endP)
        {
            UdpClient ucl = new UdpClient();
            MemoryStream memory = new MemoryStream();
            List<string> usersNameList = new List<string>();
            foreach (var us in allUsers)
            {
                usersNameList.Add(us.userName);
            }

            dataSerializer.Serialize(memory, new SendingData() { messageCommand = message, AllUsers = usersNameList });
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, Convert.ToInt32(memory.Length));

            try
            {
                server.Send(buffer, buffer.Length, endP);
                //ucl.Send(buffer, buffer.Length, endP);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Отправляет карту игроку
        /// </summary>
        /// <param name="message"></param>
        /// <param name="endP"></param>
        /// <param name="sCard"></param>
        private void SendCard(IPEndPoint endP, ShortCard sCard)
        {
            UdpClient ucl = new UdpClient();
            MemoryStream memory = new MemoryStream();

            dataSerializer.Serialize(memory, new SendingData() { messageCommand = "newCard@", card = sCard });
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, Convert.ToInt32(memory.Length));

            try
            {
                server.Send(buffer, buffer.Length, endP);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Сообщение об ошибке на сервере
        /// </summary>
        /// <param name="endP"></param>
        private void ServerStatusError()
        {
            UdpClient ucl = new UdpClient();
            MemoryStream memory = new MemoryStream();
            string message = "";

            switch (sStatus)
            {
                case ServerStatus.Started:
                    message = "server@Started";
                    break;
                case ServerStatus.GameInProgress:
                    message = "server@GIM";
                    break;
                case ServerStatus.Stopped:
                    message = "server@Stopped";
                    break;
            }

            dataSerializer.Serialize(memory, new SendingData() { messageCommand = message });
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, Convert.ToInt32(buffer.Length));

            try
            {
                server.Send(buffer, buffer.Length, remoteEndPoint);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Ход следующего игрока
        /// </summary>
        /// <param name="endP">Адресс следующего игрока</param>
        void SendYouPlay(IPEndPoint endP)
        {
            UdpClient ucl = new UdpClient();
            MemoryStream memory = new MemoryStream();

            dataSerializer.Serialize(memory, new SendingData() { messageCommand = "playersTurn@" });
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, buffer.Length);

            try
            {
                server.Send(buffer, buffer.Length, endP);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Отправка результатов и победителя игроку
        /// </summary>
        /// <param name="endP">Адрес получателя</param>
        void SendScores(IPEndPoint endP)
        {
            UdpClient ucl=new UdpClient();
            MemoryStream memory = new MemoryStream();
            string winners="winner@";
            winners+=ChooseWinner();
            List<string> winList = new List<string>();
            foreach (var item in scoresTable)
            {
                winList.Add(item.Key + " " + item.Value);
            }

            dataSerializer.Serialize(memory, new SendingData { messageCommand = winners, scoreTableSend = winList });
            memory.Position = 0;
            buffer = new byte[memory.Length];
            memory.Read(buffer, 0, buffer.Length);

            try
            {
                server.Send(buffer, buffer.Length, endP);
                memory.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string ChooseWinner()
        {
            string temp="";
            int max = -1;
            //нахождение максимального значения
            foreach (var item in scoresTable.Values)
            {
                if (item <= 21&&item>max)
                {
                    max = item;
                }
            }

            //Формирование строки со списком победителей
            temp = "\tПобедители:\nКоличетсов очков:" + max.ToString() + "\n";

            switch (max)
            {
                case -1:
                    return "Победителей нет!!!";
                default:
                    foreach (var item in scoresTable.Keys)
                    {
                        if (scoresTable[item] == max)
                        {
                            temp += string.Format("Игрок:{0} очки:{1\n", scoresTable[item], item);
                        }
                    }
                    return temp;
            }
            
        }


        #endregion

        /*  private void SendReplaseUsers(SendingData _send, XmlSerializer _dataSerializer, MemoryStream _stream, List<string> users)
          {
              UdpClient client = new UdpClient();
              SendData sd = new SendData() { name = "SERVER", AllUsers = users, messageCommand = "replaseusers" };
              _dataSerializer.Serialize(_stream, sd);
              _stream.Position = 0;
              byte[] _bytes = new byte[_stream.Length];
              _stream.Read(_bytes, 0, Convert.ToInt32(_stream.Length));
              //MessageBox.Show(_bytes.Length + " " + _stream.Length);

              try
              {
                  foreach (IPEndPoint ep in allUsers)
                  {
                      //Thread.Sleep(500);
                      //client.Send(_bytes, _bytes.Length, ep);
                      _server.Send(_bytes, _bytes.Length, ep);
                  }

              }
              catch (SocketException se)
              {
                  if (se.ErrorCode != 1004) // игнорирование ошибки WSACancelBlockingCall (библиотека WINSOCK.DLL) - не верные аргументы
                  {
                      MessageBox.Show("SERVER SENDER PIOPLE \n" + se.Message);
                  }
              }
              catch (Exception exs)
              {
                  MessageBox.Show(exs.Message);
              }

          }*/

        /// <summary>
        /// Прослушивание входящих сообщений
        /// </summary>
         void Listener()
        {

            
            SendingData send = new SendingData();

            try
            {

                while (true)
                {
                    buffer = server.Receive(ref remoteEndPoint);
                    messageCommand=encodingMessage.GetString(buffer);
                   
                    string[] detailedMessageCommand=messageCommand.Split('@');
                    
                    //Сохраняет имя пользователя
                    if (detailedMessageCommand[0] != "newPlayer")
                    {
                        foreach(var user in allUsers)
                            if(user.userIpEndPoint==remoteEndPoint)
                            {
                                userName = user.userName;
                                break;
                            }
                         
                    }
                       
                    switch (detailedMessageCommand[0])
                    {

                        case "newPlayer": //подключение нового пользователя
                            NewPlayerCase(detailedMessageCommand);
                            break;
                            

                        case "disconnect": //отключение пользователя
                            PlayerDisconnectedCase(detailedMessageCommand);
                            break;

                        case "hit":
                            HitCase();
                            break;//взять еще карту

                        case "stop":
                            StopCommandChoose(); 
                            break;

                        default:
                            break;
                    }




                    //Dispatcher.BeginInvoke(new Action(delegate { tbStatistics.Text = DateTime.Now.ToShortTimeString() + " " + _messageCommand + "\r\n" + tbStatistics.Text; }));
                }
            }
            catch (SocketException se)
            {
                if (se.ErrorCode != 1004) // игнорирование ошибки WSACancelBlockingCall (библиотека WINSOCK.DLL) - не верные аргументы
                {
                    MessageBox.Show("SERVER\n" + se.Message);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("SERVER\n" + e.Message);
            }
        }

        #region Listner cases
        /// <summary>
        /// Добаввление нового игрока
        /// </summary>
        /// <param name="detailedMessageCommand"></param>
        private void NewPlayerCase(string[] detailedMessageCommand)
        {
            if (sStatus == ServerStatus.Started &&
                                allUsers.Exists(x => x.userIpEndPoint.Port == remoteEndPoint.Port) == false)
            {

                {
                    ReplaceCommandMultiCast();
                }
            }
            else
            {
                ServerStatusError();
            }
    }
        /// <summary>
        /// Игрок отключился
        /// </summary>
        /// <param name="detailedMessageCommand"></param>
        private void PlayerDisconnectedCase(string[] detailedMessageCommand)
        {

            ReplaceCommandMultiCast();
            if(!PlayerAlreadyPlayed())
                PlayerScoreRecieve(detailedMessageCommand[1], remoteEndPoint.Port.ToString(), "22");
            
            
        }
        /// <summary>
        /// Удаление пользователя из списка активных
        /// </summary>
        void RemovingUser()
        {
            foreach (var user in allUsers)
            {
                if (user.userIpEndPoint.Port == remoteEndPoint.Port)
                {
                    allUsers.Remove(user);
                    break;
                }
            }
        }

       
        /// <summary>
        /// Игрок просит карту
        /// </summary>
        private void HitCase()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                TextBoxGameStatistic.Text = DateTime.Now.ToShortTimeString() + ": " + userName + " receive cards \r\n";
            }));
            SendCard(remoteEndPoint, serverDeck.GetShortCard());
        }

        /// <summary>
        /// Игрок закончил ход
        /// </summary>
        /// <param name="detailedMessageCommand"></param>
        private void PlayerStopsCase(string[] detailedMessageCommand)
        {
            PlayerScoreRecieve(detailedMessageCommand[1], remoteEndPoint.Port.ToString(), detailedMessageCommand[2]);
            StopCommandChoose();
        }
        /// <summary>
        /// Запись очков пользователя
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <param name="port">Номер порта </param>
        /// <param name="score"></param>
        private void PlayerScoreRecieve(string name, string port, string score)
        {
            scoresTable.Add(name+" - "+port, Convert.ToInt32(score));
        }

        /// <summary>
        /// Остались ли еще игроки?
        /// </summary>
        private void StopCommandChoose()
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (allUsers[i].userIpEndPoint.Port == remoteEndPoint.Port)
                {
                    if (i + 1 < allUsers.Count)
                    {
                        SendYouPlay(allUsers[i + 1].userIpEndPoint);
                    }
                    else
                    {   
                        foreach(var user in allUsers)
                            SendScores(user.userIpEndPoint);
                    }
                }
            }
        }
         /// <summary>
        /// Игрок уже сделал свой ход
        /// </summary>
        /// <returns>Игрок ходил</returns>
        bool PlayerAlreadyPlayed()
        {
            foreach (string namesT in scoresTable.Keys)
            {
                if (namesT.Contains(remoteEndPoint.Port.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Отсылка сообщения об изменении списка игроков всем игрокам
        /// </summary>
        void ReplaceCommandMultiCast()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                ListBoxPlayers.Items.Clear();
                foreach (var user in allUsers)
                {
                    ListBoxPlayers.Items.Add(user.userName);
                }
            }));
            foreach (var user in allUsers)
            {
                Thread.Sleep(200);
                SendNamesList("replaseusers", user.userIpEndPoint);
            }
        }
        #endregion
    }
}
