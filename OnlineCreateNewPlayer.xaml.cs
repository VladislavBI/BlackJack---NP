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

namespace GameTable
{
    /// <summary>
    /// Interaction logic for OnlineCreateNewPlayer.xaml
    /// </summary>
    public partial class OnlineCreateNewPlayer : Window
    {
        GamesProcess game;
        public OnlineCreateNewPlayer(GamesProcess game)
        {
            InitializeComponent();
            this.game = game;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            game.HumanPlayerCreate(TextBoxName.Text);
            WaitRoomWindow wr = new WaitRoomWindow(game);
            wr.Show();
            this.Close();
        }
    }
}
