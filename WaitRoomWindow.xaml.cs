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
using System.Windows.Shapes;
using GameTable.NamespacePlayers;
namespace GameTable
{
    using GameTable.NamespaceGame;
using GameTable.OnlineGame;
    /// <summary>
    /// Interaction logic for WaitRoomWindow.xaml
    /// </summary>
    public partial class WaitRoomWindow : Window
    {
        GamesProcess curGame { get; set; }
        OnlineServer server = new OnlineServer();
        public WaitRoomWindow(GamesProcess game)
        {
            InitializeComponent();
            DelegatesData.HandlerServerStatusChangeView =
                new DelegatesData.ServerStatusChangeView(StatusChangeEffect);
            curGame = game;
            PlayersListLBoxFill();
        }

          private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            server.InitializeServer();
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
            ClearListBox();
            
            List<string>players=curGame.GetActivePlayersList();

            foreach (var item in players)
            {
                ListBoxPlayers.Items.Add(item);
            }
        }

        /// <summary>
        /// Изменение окна при изменении статуса сервера
        /// </summary>
        /// <param name="serverWork">Сервер работает?</param>
        public void StatusChangeEffect(bool serverWork)
        {
            if (serverWork)
            {
                ButtonStartGame.IsEnabled = false;
                ButtonStopGame.IsEnabled = true;
                TextBoxGameStatistic.AppendText(DateTime.Now.ToShortTimeString() + " Запуск сервера\n");
            }
            else 
            {
                ButtonStartGame.IsEnabled = true;
                ButtonStopGame.IsEnabled = false;
                TextBoxGameStatistic.AppendText(DateTime.Now.ToShortTimeString() + " Остановка сервера\n");
            }
        }

        private void ButtonStopGame_Click(object sender, RoutedEventArgs e)
        {
            StatusChangeEffect(false);
            ClearListBox();
        }
      
    }
}
