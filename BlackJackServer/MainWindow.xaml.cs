using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJackServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Очистка Окна со списком имен 
        /// </summary>
        public void ClearListBox()
        {
            ListBoxPlayers.Items.Clear();
        }

        /// <summary>
        /// Создание списка имен игроков
        /// </summary>
        public void PlayersListLBoxFill()
        {        
        }

        /// <summary>
        /// Изменение окна при изменении статуса сервера
        /// </summary>
        /// <param name="serverWork">Сервер работает?</param>
        public void StatusChangeEffect(ServerStatus status)
        {
            switch (status)
            {
                case ServerStatus.Started:
                    ButtonStartServer.IsEnabled = false;
                    ButtonStartGame.IsEnabled = true;
                    TextBoxGameStatistic.AppendText(DateTime.Now.ToShortTimeString() + " Запуск сервера\n");
                    break;

                case ServerStatus.GameInProgress:
                    ButtonStartGame.IsEnabled = false;
                    ButtonStopGame.IsEnabled = true;
                    TextBoxGameStatistic.AppendText(DateTime.Now.ToShortTimeString() + " Начало игры\n");
                    break;

                case ServerStatus.Stopped:
                    ButtonStopGame.IsEnabled = false;
                    ButtonStartServer.IsEnabled = true;
                    TextBoxGameStatistic.AppendText(DateTime.Now.ToShortTimeString() + " Закрытие сервера\n");
                    break;
            }
            
        }


        private void ButtonStartServer_Click(object sender, RoutedEventArgs e)
        {
            InitializeServer();
            StatusChangeEffect(ServerStatus.Started);
            sStatus = ServerStatus.Started;
        }

        private void ButtonStartGame_Click(object sender, RoutedEventArgs e)
        {
            StatusChangeEffect(ServerStatus.GameInProgress);
            NewServerDeckCreate();
            sStatus = ServerStatus.GameInProgress;
            StartGame();
        }

        private void ButtonStopGame_Click(object sender, RoutedEventArgs e)
        {
            StatusChangeEffect(ServerStatus.Stopped);
            sStatus = ServerStatus.Stopped;
            server.Close();
            
        }
    }
}
