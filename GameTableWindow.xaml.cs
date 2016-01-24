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
using GameTable.NamespacePlayers;
namespace GameTable
{
    /// <summary>
    /// Interaction logic for GameTableWindow.xaml
    /// </summary>
    public partial class GameTableWindow : Window
    {
        GamesProcess game;
        public GameTableWindow(GamesProcess game)
        {
            InitializeComponent();
            DelegatesData.HandlerCreateTableViewForCurrentPlayer =
                new DelegatesData.CreateTableViewForCurrentPlayer(ViewForPlayerCreating);
            DelegatesData.HandlerWinnerPlayerShow = 
                new DelegatesData.WinnerPlayerShow(ShowWinner);
            this.game = game;
            game.GameStart(1);
        }



        #region Внешний вид стола
        /// <summary>
        /// Создание внешнего вида стола для игрока
        /// </summary>
        public void ViewForPlayerCreating()
        {
            HumanPlayer curPlayer = game.GetCurrentHumanPlayer();
            NonCardViewCreate(curPlayer);
            CardViewCreate(curPlayer);

        }
        /// <summary>
        /// Внешний вид стола без учета карт
        /// </summary>
        /// <param name="curPlayer">Текущий игрок</param>
        void NonCardViewCreate(HumanPlayer curPlayer)
        {
            TextBlockName.Text = curPlayer.playersName;
            TextBlockScore.Text = curPlayer.GetPlayersPoints().ToString();
        }
        /// <summary>
        /// Внешний вид карт на столе
        /// </summary>
        /// <param name="curPlayer">Текущий игрок</param>
        void CardViewCreate(HumanPlayer curPlayer)
        {
            StackplayersCard.Children.Clear();
            foreach (CardDeck.CardFactory card in curPlayer.cardsOnHand.gameDeck)
            {
                Grid cardGrid = new Grid();
                
                cardGrid.Margin = new Thickness(5);
                cardGrid.Background = new SolidColorBrush(Colors.Yellow);


                TextBlock cardBlock = new TextBlock();
                cardBlock.Text = card.ToString();
                cardBlock.FontSize = 16;
                cardBlock.VerticalAlignment = VerticalAlignment.Center;
                cardBlock.Background = new SolidColorBrush(Colors.Yellow);

                StackplayersCard.Children.Add(cardGrid);
                cardGrid.Children.Add(cardBlock);

            }
        }
        #endregion

        private void ButtonGetCard_Click(object sender, RoutedEventArgs e)
        {
            game.GetCurrentHumanPlayer().AchievingOfCard();
            
        }

        private void ButtonPass_Click(object sender, RoutedEventArgs e)
        {
            game.GetCurrentHumanPlayer().PlayerStopsTurn();
        }

        public void ShowWinner()
        {
            game.AnnouncementOfWinners();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            game.GameRestart();
        }
        
    }
}
