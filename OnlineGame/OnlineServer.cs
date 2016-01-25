using GameTable.CardDeck;
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

namespace GameTable.OnlineGame
{
    [Serializable]
    public class SendingData
    {
        public string messageShift { get; set; }
        public CardFactory card { get; set; }
        public int account { get; set; }
        public string name { get; set; }
        public int ToUser { get; set; }
        public List<string> AllUsers { get; set; }
    }
    class OnlineServer
    {
        int localPort;//
        UdpClient server;//
        IPEndPoint remoteEndPoint;//
        string userName;//
        string messageShift;//
        byte[] bufer;//
        List<IPEndPoint> _allUsers = new List<IPEndPoint>();
        XmlSerializer _dataSerializer = new XmlSerializer(typeof(SendingData));
        List<string> users = new List<string>();

        #region Открытие сервера

        #endregion
        /// <summary>
        /// Создание нового сервера
        /// </summary>
        public void InitializeServer()
        {
            StartUdpClient();
            StartListener();
            DelegatesData.HandlerServerStatusChangeView(true);
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
            /*Thread thread = new Thread(Listener);
            thread.IsBackground = true;
            thread.Start();*/
        }
    }
}
