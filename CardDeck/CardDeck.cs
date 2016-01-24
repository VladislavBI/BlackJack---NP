using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameTable.CardDeck
{
    /// <summary>
    /// Абстрактный класс для колоды карт
    /// </summary>
    public abstract class CardDeck
    {
        /// <summary>
        /// Игровая колода карт
        /// </summary>
        public Stack<CardFactory> gameDeck{get;set;}

    }

    /// <summary>
    /// Общая колода карт
    /// </summary>
    public class GeneralDeck: CardDeck
    {
        public GeneralDeck ()
	    {
            gameDeck = new Stack<CardFactory>();
            //создание колоды
            CreateDeck();
            ShufflingDeck();

            //создание делегата для вытягивания карты
            CreateGetCardDelegate();
	    }


        /// <summary>
        /// Создает новую колоду всех карт
        /// </summary>
        public void CreateDeck()
        {
            foreach(CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach(CardHierarchy hier in Enum.GetValues(typeof(CardHierarchy)))
                {
                    if(hier!=CardHierarchy.Ace)
                        gameDeck.Push(new OnePointCard(hier, suit));
                    else
                        gameDeck.Push(new TwoPointCard(hier, suit));
                }
            }
        }

        /// <summary>
        /// Тасует уже имеющуюся колоду карт
        /// </summary>
        public void ShufflingDeck()
        {
            gameDeck = CardGeneralInfoCreator.ShufflingOfTheDeck(gameDeck);
        }

        /// <summary>
        /// Взять карту из колоды
        /// </summary>
        /// <returns>верхняя карта</returns>
        public CardFactory GetCard() 
        {
            return gameDeck.Pop();
        }

        /// <summary>
        /// Создание делегата для вызова карты
        /// </summary>
        public void CreateGetCardDelegate()
        {
            DelegatesData.HandlerGetingCardFromGenDeck = new DelegatesData.GetingCardFromGenDeck(GetCard);
        }
    }
    

    /// <summary>
    /// Колода конкретных игроков (в том числе и дилера)
    /// </summary>
    public class PlayersDeck:CardDeck
    {
        public PlayersDeck()
        {
            gameDeck = new Stack<CardFactory>();
        }
        /// <summary>
        /// Наличие туза, если true - другой вариант подсчета очков
        /// </summary>
        bool deckHasAce = false;

        /// <summary>
        /// Добавляет карту в колоду игрока
        /// </summary>
        /// <param name="cf">новая карта</param>
        public void AddCardToDeck(CardFactory cf)
        {
            if (cf.thisCardHierarchy == CardHierarchy.Ace)
                deckHasAce = true;
            gameDeck.Push(cf);
        }

        /// <summary>
        /// очистить коолоду после 1ой партии
        /// </summary>
        public void NullifyDeck() 
        {
            gameDeck.Clear();
        } 

        #region подсчет очков
                /// <summary>
                /// Выбор способа подсчета и подсчет общих очков карт в колоде 
                /// </summary>
                /// <returns>Общее количество очков</returns>
        public int GetTotalPoints()
                {
                    int points=0;
                    if (deckHasAce == false)
                        return points = CalculateNonAceDecksPoints(points);
                    else
                        return points = CalculateAceDecksPoints(points);
                }

        /// <summary>
        /// Подсчет очков всех карт колоды, не являющихся тузами  
        /// </summary>
        /// <param name="points">Количество очков до подсчета</param>
        /// <returns>Количество очков после подсчета</returns>
        int CalculateNonAceDecksPoints(int points)
        {
            foreach (CardFactory card in gameDeck)
            {
                if (card.thisCardHierarchy != CardHierarchy.Ace)
                    points += card.GetNonAceCardPoints();
            }
            return points;
        }

        /// <summary>
        /// Подсчет очков всех карт колоды, не являющихся тузами  
        /// </summary>
        /// <param name="points">Количество очков до подсчета</param>
        /// <returns>Количество очков после подсчета</returns>
        int  CalculateAceDecksPoints(int points)
        {
            //Подсчет очков бех тузов
            int tempPoints = CalculateNonAceDecksPoints(points);
            //подсчет очков с тузами
            tempPoints += SummingAceCards(tempPoints);
            return tempPoints;
 
        }
        /// <summary>
        /// Выбор методики подсчета и подсчет суммы очков с тузами 
        /// </summary>
        /// <param name="points">Очки до подсчета</param>
        /// <returns>Очки после подсчета</returns>
        int SummingAceCards(int points)
        {
            //Создание массива тузов исходной колоды
            List<TwoPointCard> AcesInTheDeck = CardGeneralInfoCreator.FindAllAces(gameDeck);

            //Подсчет очков и возврат наиболее подходящего количества очков
            return points = points + 11 + AcesInTheDeck.Count - 1 <= 21
                ? points + 11 + AcesInTheDeck.Count - 1 : points + AcesInTheDeck.Count;
        }

        #endregion
        
    }
}






