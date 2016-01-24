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
    /// Interaction logic for CreateNewHumanPlayer.xaml
    /// </summary>
    public partial class CreateNewHumanPlayer : Window
    {
        GamesProcess game;
        public CreateNewHumanPlayer(GamesProcess game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            game.HumanPlayerCreate(TextBoxName.Text);
            GoToTable();
        }

        void GoToTable()
        {
            GameTableWindow table = new GameTableWindow(game);
            table.Show();
            this.Close();
        }
    }
}
