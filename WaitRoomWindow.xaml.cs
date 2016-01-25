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
    /// <summary>
    /// Interaction logic for WaitRoomWindow.xaml
    /// </summary>
    public partial class WaitRoomWindow : Window
    {
        GamesProcess curGame { get; set; }
        public WaitRoomWindow(GamesProcess game)
        {
            InitializeComponent();
            curGame = game;
            PlayersListLBoxFill();
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
    }
}
