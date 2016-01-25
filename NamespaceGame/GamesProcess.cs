using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTable.NamespacePlayers;
using GameTable.CardDeck;
using System.Windows;

namespace GameTable.NamespaceGame
{
    abstract class BaseGame { }
    public class GamesProcess:BaseGame
    {
        #region Поля класса
        /// <summary>
        /// Список всех игроков
        /// </summary>
        List<Player> ActivePlayersList = new List<Player>();
        /// <summary>
        /// Номера игроков-людей
        /// </summary>
        Queue<int> HumanPlayerNumber = new Queue<int>();
        /// <summary>
        /// Номер текущего игрока
        /// </summary>
        int currentPlayerNumber=-1;
        /// <summary>
        /// Таблица результатов (номер игрока/количество очков)
        /// </summary>
        Dictionary<int, int> scoreTable = new Dictionary<int, int>();
        /// <summary>
        /// Игровая колода
        /// </summary>
        GeneralDeck genDeck;
        #endregion

        #region Создание стола
        /// <summary>
        /// Действия в начале игры
        /// </summary>
        /// <param name="CompPlayersQty"></param>
        public void GameStart(int CompPlayersQty)
        {
            CreateNewDeck();
            
            //Присвоение делегата для следующего хода
            DelegatesData.HandlerPlayerIsMoreThanEnough =
                new DelegatesData.PlayerIsMoreThanEnough(TurnComesToNextPlayer);

            //Создание игрового стола
            BotPlayersCreate(CompPlayersQty);
            GameStartsCommon();
            

        }
        /// <summary>
        /// Создание следующей партии
        /// </summary>
        public void GameRestart() 
        {
            foreach (var item in ActivePlayersList)
            {
                item.cardsOnHand.NullifyDeck();
            }
            HumanPlayerQueueReCreate();
            GameStartsCommon();

        }

        /// <summary>
        /// Общее в двух классах-началах игры
        /// </summary>
        void GameStartsCommon()
        {
            //Изменение состояния кнопок
            DelegatesData.HandlerTableButtonsIsEnanbleChange(true);
            GetStartCards();
            PlayersPoolCreate();
        }
        /// <summary>
        /// Создание новой колоды
        /// </summary>
        public void CreateNewDeck()
        {
            genDeck = new GeneralDeck();
        }
        /// <summary>
        /// Казино раздает по 2 карты всем игрокам
        /// </summary>
        void GetStartCards()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Player pl in ActivePlayersList)
                {
                    pl.PutCardInDeck(genDeck.GetCard());
                }
            }
        }
        /// <summary>
        /// Создание поля игрока - привязка к WPF
        /// </summary>
        public void PlayersPoolCreate()
        {
            DelegatesData.HandlerCreateTableViewForCurrentPlayer();
        }
   
        /// <summary>
        /// Заполнение таблицы всех результатов игроков
        /// </summary>
        public void ResultsCalculating()
        {
            for (int i = 0; i < ActivePlayersList.Count; i++)
			{
                scoreTable.Add(i, ActivePlayersList[i].GetPlayersPoints());
			} 
        }
        /// <summary>
        /// Создание новой общей колоды карт
        /// </summary>
       
        #endregion

        #region Выбор победителя
        /// <summary>
        /// Объявление победителей
        /// </summary>
        public void AnnouncementOfWinners()
        {
            List<Player> winnerList = WinnerChoose();
            switch (winnerList.Count)
            {
                case 0:
                    MessageBox.Show("Победитей нет!");
                    break;
                case 1:
                    MessageBox.Show(string.Format("Победил игрок {0} с {1} очками", 
                        winnerList[0].playersName, winnerList[0].GetPlayersPoints()));
                    break;
                default:
                    string winnerAnnounce = 
                        "Победили игроки с количеством очков " + winnerList[0].GetPlayersPoints()+ "\n";
                    foreach (Player pl in winnerList)
                    {
                        winnerAnnounce += pl.playersName + "\n";
                    }
                    break;
            }
        }
        /// <summary>
        /// Процесс выбора победителей
        /// </summary>
        /// <returns>Список победителей</returns>
        List<Player> WinnerChoose()
        {
            int currentWinScore = 0;
            List<Player> winnersList = new List<Player>();
            currentWinScore = BestScoreSeek();
            winnersList = WinnersListCreating(currentWinScore);
            return winnersList;
        }

        /// <summary>
        /// Поиск лучшего счета среди игроков
        /// </summary>
        /// <returns>Лучший счет</returns>
        int BestScoreSeek()
        {
            int test = 0;
            foreach (Player pl in ActivePlayersList)
            {
                if (pl.GetPlayersPoints() <= 21 && pl.GetPlayersPoints() > test)
                    test = pl.GetPlayersPoints();
            }
            return test;
        }

        /// <summary>
        /// Составление списка победителей
        /// </summary>
        /// <param name="BestScore">Лучший счет</param>
        /// <returns>Список победителей</returns>
        List<Player> WinnersListCreating(int BestScore)
        {
            //Нахождение всех игроков с определенным счетом 
            var WinnersList = from pl in ActivePlayersList where pl.GetPlayersPoints() == BestScore select pl;
            List<Player> winners = new List<Player>();
            //проверка на дилера (при равном счете он побеждает)
            foreach (Player pl in WinnersList)
            {
                if (pl is Dealer)
                {
                    winners.Clear();
                    winners.Add(pl);
                    return winners;
                }
                winners.Add(pl);
            }
            return winners;
        }
        #endregion

        #region Создание игроков
        /// <summary>
        /// Создание нового игрока-человека
        /// </summary>
        /// <param name="name"></param>
        public void HumanPlayerCreate(string name)
        {
            //Создание нового игрока типа HumanPlayer
            ActivePlayersList.Add(new HumanPlayer(name));

            //Фиксация номера этого игрока и установка его как текущего (если других небыло)
            HumanPlayerQueueFix(ActivePlayersList.Count - 1);
        }

        /// <summary>
        /// Фиксация номера игрока человека
        /// </summary>
        public void HumanPlayerQueueFix(int plNumber)
        {
            HumanPlayerNumber.Enqueue(plNumber);
            if (HumanPlayerNumber.Count == 1)
                currentPlayerNumber = HumanPlayerNumber.Peek();
        }
        /// <summary>
        /// Пересоздание списка номеров игроков-людей
        /// </summary>
        public void HumanPlayerQueueReCreate()
        {
            HumanPlayerNumber.Clear();
            for (int i = 0; i < ActivePlayersList.Count; i++)
            {
                if (ActivePlayersList[i] is HumanPlayer)
                {
                    HumanPlayerQueueFix(i);
                }
            }         
           
        }
        /// <summary>
        /// Добавление дилера и ботов в игру
        /// </summary>
        /// <param name="quantity">Количество ботов</param>
        void BotPlayersCreate(int quantity)
        {
            //Проверка на режим дуэли - игрок против игрока
            if (quantity != 0)
            {
                //добавление в игру дилера
                ActivePlayersList.Add(new Dealer());

                //добавление ботов
                for (int i = 0; i < quantity - 1; i++)
                {
                    ActivePlayersList.Add(new CompPlayer());
                }
            }
        }
#endregion

        #region Действия игрока-человека

        /// <summary>
        /// Возврат играющего игрока
        /// </summary>
        /// <returns>Текущий игрок</returns>
        public HumanPlayer GetCurrentHumanPlayer()
        {
            return ActivePlayersList[currentPlayerNumber] as HumanPlayer;
        }
        /// <summary>
        /// Игрок берет новую карту
        /// </summary>
        public void HumanPlayerGetsCard()
        {
            
                ActivePlayersList[currentPlayerNumber].AchievingOfCard();
           
        }
        /// <summary>
        /// Игрок заканчивает ход
        /// </summary>
        public void HumanStopPlaying()
        {
            //игрок заканчивает свой ход
            ActivePlayersList[currentPlayerNumber].PlayerStopsTurn();
        }
        /// <summary>
        /// Переход хода к следующему игроку
        /// </summary>
        public void TurnComesToNextPlayer()
        {
            currentPlayerNumber = HumanPlayerNumber.Dequeue();
            //Назначение следующего активного игрока, создание его игрового стола
            if (HumanPlayerNumber.Count != 0)
            {
                PlayersPoolCreate();
            }
            //Если игроков нет - играют боты
            else
            {
                DelegatesData.HandlerTableButtonsIsEnanbleChange(false);
                BotsAreMoving();
            }
        }
        #endregion

        /// <summary>
        /// Ход ботов
        /// </summary>
        public void BotsAreMoving()
        {
            //список всех ботов за столом
            var botsPlayers = ActivePlayersList.FindAll(item => item is CompPlayer);
            //Проверка - есть ли боты
            if (botsPlayers.Count > 0)
            {
                //каждый бот делает свой ход
                foreach (CompPlayer pl in botsPlayers)
                {
                    pl.CompsTurn();
                }
            }
            DelegatesData.HandlerWinnerPlayerShow();
        }

        /// <summary>
        /// Возврат списка имен всех игроков
        /// </summary>
        /// <returns>Список имен игроков</returns>
        public List<String> GetActivePlayersList()
        {
            List<string> temp=new List<string>();
            foreach (var pl in ActivePlayersList)
	        {
		        temp.Add(pl.playersName);
	        }
            return temp;
        }
    }
}
