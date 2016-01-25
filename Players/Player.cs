using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameTable.Players
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

        public Player(string name)
        {
            playersName = name;
        }

        

        /// <summary>
        /// Игрок заканчивает свой ход
        /// </summary>
        public void PlayerStopsTurn()
        {
            playerIsStillIngame = false;
            playerSaysThatHeIsEnough();
        }

        #region Фразы игрока
        /// <summary>
        /// Игрок заявляет о своем намерении взять карту
        /// </summary>
        /// <param name="card">Добавляемая карта</param>
        public void playerAttempsToGetAnotherCard()
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
            //Результаты хода
            TurnsResults();
        }

        /// <summary>
        /// Взятие карты из общей колоды с помощью делегата.
        /// На доработке!!!!!
        /// </summary>
        /// <returns>Карта из колоды</returns>
        CardDeck.CardFactory GettingCardFromGeneralDeck()
        {
            return new CardDeck.OnePointCard(CardHierarchy.C10, CardSuit.clubs);
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
        int GetPlayersPoints()
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
        /// Ход бота, принимает решение - ходить/закончить ход
        /// </summary>
        public void CompsTurn()
        {
            //ходить, запас есть
            if (DecisionToGetAnotherCard())
                AchievingOfCard();
                
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
            if (GetPermissibleValue() <= cardsOnHand.GetTotalPoints())
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
        #endregion
    }

    public class Dealer : CompPlayer
    {
        public Dealer():base()
        {
            isDealer = true;
        }
    }
}
