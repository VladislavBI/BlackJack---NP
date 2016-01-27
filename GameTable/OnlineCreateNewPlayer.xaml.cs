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
using GameTable.NamespaceGame;
using GameTable.OnlineGame;

namespace GameTable
{
    /// <summary>
    /// Interaction logic for OnlineCreateNewPlayer.xaml
    /// </summary>
    public partial class OnlineCreateNewPlayer : Window
    {
        OnlineGame.OnlineGame game;
        public OnlineCreateNewPlayer(OnlineGame.OnlineGame game)
        {
            InitializeComponent();
            this.game = game;
            DelegatesData.HandlerPlayersListRefresh = new DelegatesData.PlayersListRefresh(PlayerListFill);
            DelegatesData.HandlerGameTableOpen = new DelegatesData.GameTableOpen(GoToGameTable);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            game.ConnectToServer(TextBoxName.Text, TextBoxMyPort.Text, 
                TextBoxRemotePort.Text, TextBoxRemoteIPAddress.Text);
            ButtonConnect.IsEnabled = false;
            ButtonDisconnect.IsEnabled = true;
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            
            game.CloseServer();
            ButtonConnect.IsEnabled = true;
            ButtonDisconnect.IsEnabled = false;
        }

        public void PlayerListFill(SendingData send)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                TextBlockInGame.Text = string.Empty;
                foreach (string user in send.AllUsers)
                {
                    TextBlockInGame.Text += user + "\r\n";
                }
            }));
        }

        private void TextBoxMyPort_Loaded(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            TextBoxMyPort.Text=r.Next(7777, 20000).ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            game.CloseServer();
            MainWindow mw = new MainWindow();
            mw.Show(); 
            this.Close();
        }

        void GoToGameTable()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                GameTableWindow gt = new GameTableWindow(game, 0);
                gt.Show();
                this.Close();
            }));
            
        }
    }
}
