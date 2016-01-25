using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameTable.NamespacePlayers
{
    /// <summary>
    /// Базовый игрок
    /// </summary>
    public abstract class Player
    {
        public string playersName;
        public CardDeck.PlayersDeck cardsOnHand= new CardDeck.PlayersDeck();
        public bool playerIsStillIngame=true;
        public bool isDealer = false;

        /// <summary>
        /// Конструктор для ботов
        /// </summary>
        public Player()
        { 
            playersName = "Безымянный"; 
        }

        /// <summary>
        /// Конструктор для людей
        /// </summary>
        /// <param name="name">Имя игрока</param>
        public Player(string name)
        {
            if (name == "")
                playersName = "Безымянный"; 
            else
                playersName = name;
        }

        

        /// <summary>
        /// Игрок заканчивает свой ход
        /// </summary>
        public virtual void PlayerStopsTurn()
        {
            playerSaysThatHeIsEnough();
            playerIsStillIngame = false;
            DelegatesData.HandlerPlayerIsMoreThanEnough();

        }

        #region Фразы игрока
        /// <summary>
        /// Игрок заявляет о своем намерении взять карту
        /// </summary>
        /// <param name="card">Добавляемая карта</param>
        void playerAttempsToGetAnotherCard()
        {
            MessageBox.Show("Еще одну карту!");
        }

        /// <summary>
        /// Игрок заканчивает свой ход
        /// </summary>
        void playerSaysThatHeIsEnough() 
        {
            MessageBox.Show("Мне не нужны карты!"); 
        }

        #endregion

        #region Взятие карты игроком
        
        /// <summary>
        /// Процесс взятия карты из общей колоды 
        /// и добавления в свою
        /// </summary>
        /// <param name="card">Добавляемая карта</param>
        public void AchievingOfCard()
        {
            //Игрок заявляет о намерении взять карту
            playerAttempsToGetAnotherCard();
            //Игрок берет карту
            PutCardInDeck(GettingCardFromGeneralDeck());
            //Добавление карты на стол
            DelegatesData.HandlerCreateTableViewForCurrentPlayer();
            //Результаты хода
            TurnsResults();

        }

        /// <summary>
        /// Взятие карты из общей колоды с помощью делегата.
        /// </summary>
        /// <returns>Карта из колоды</returns>
        protected CardDeck.CardFactory GettingCardFromGeneralDeck()
        {
            return DelegatesData.HandlerGetingCardFromGenDeck(); ;
        }

        /// <summary>
        /// Игрок кладет одну карту себе в колоду
        /// </summary>
        /// <param name="card">Добавляемая карта</param>
        public void PutCardInDeck(CardDeck.CardFactory card)
        {
            cardsOnHand.AddCardToDeck(card);
        }
        
        /// <summary>
        /// Подсчет результатов хода
        /// </summary>
        void TurnsResults()
        {
            if (cardsOnHand.GetTotalPoints() >= 21)
            {
                PlayerStopsTurn();
            }
           
        }

        /// <summary>
        /// Возвращает количество очков этого игрока
        /// </summary>
        /// <returns></returns>
        public int GetPlayersPoints()
        {
            return cardsOnHand.GetTotalPoints();
        }

        #endregion
       

    }

    /// <summary>
    /// Игрок, управляемый компьютером
    /// </summary>
    public class CompPlayer : Player 
    {
        /// <summary>
        /// Конструктор для ботов, имя выбирается из списка
        /// </summary>
        public CompPlayer()
        {
            Random r = new Random();
            playersName = CardGeneralInfoCreator.botsName[r.Next(0, CardGeneralInfoCreator.botsName.Count - 1)];
        }

        /// <summary>
        /// Ход бота, принимает решение - ходить/закончить ход
        /// </summary>
        public void CompsTurn()
        {
            //ходить, запас есть
            if (DecisionToGetAnotherCard())
                PutCardInDeck(GettingCardFromGeneralDeck());
                
            //закончить ход, запаса нет
            else
                PlayerStopsTurn();
        }

        #region Мозговой процесс бота
        /// <summary>
        /// бот делает решение о ходе
        /// </summary>
        /// <returns>Решение бота</returns>
        bool DecisionToGetAnotherCard()
        {
            if (GetPermissibleValue() >= cardsOnHand.GetTotalPoints())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Бот выбирает свой порог риска
        /// </summary>
        /// <returns>Пороговое значение очков</returns>
        int GetPermissibleValue()
        {
            Random r = new Random();
            return r.Next(12, 17);
        } 
       /// <summary>
       /// Бот заканчивает ход - ануляются дествия метода для игрока
       /// </summary>
        public override void PlayerStopsTurn()
        {}
        #endregion
    }

    /// <summary>
    /// Игрок - дилер (управляется компьютером)
    /// </summary>
    public class Dealer : CompPlayer
    {
        public Dealer():base()
        {
            playersName += " дилер";
            isDealer = true;
        }
    }

    /// <summary>
    /// Игрок, управляемый человеком
    /// </summary>
    public class HumanPlayer : Player
    {
        /// <summary>
        /// Конструктор для людей, имя приход извне (задает игрок)
        /// </summary>
        /// <param name="name">Имя игрока</param>
        public HumanPlayer(string name)
        {
            Random r = new Random();
            if (name == "")
                playersName = "Безымянный"+r.Next(1, 999999).ToString(); 
            else
                playersName = name;
        }
    }
}
