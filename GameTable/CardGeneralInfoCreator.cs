using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTable.CardDeck;

namespace GameTable
{
    /// <summary>
    /// иерархия карт
    /// </summary>
    public enum CardHierarchy { C2, C3, C4, C5, C6, C7, C8, C9, C10, Jack, Queen, King, Ace}

    /// <summary>
    /// Масти карт :clubs=♣, diamonds=♦, hearts=♥, pikes=♠
    /// </summary>
    public enum CardSuit { clubs, diamonds, hearts, pikes }

    /// <summary>
    /// класс для работы с Колодами и Картами
    /// </summary>
    static class CardGeneralInfoCreator
    {
        /// <summary>
        /// Список имен ботов
        /// </summary>
        static public List<string> botsName = new List<string>()
        {
            "Ваня",
            "Вася",
            "Петя",
            "Женя",
            "Даша",
            "Маша",
            "Катя",
            "Лена"
        };
        /// <summary>
        /// Нормальный вид иерархии карты
        /// </summary>
        static Dictionary<CardHierarchy, string> CardHierarchyNormalView =
            new Dictionary<CardHierarchy, string>()
        {
            {CardHierarchy.C2, "2"},
            {CardHierarchy.C3, "3"},
            {CardHierarchy.C4, "4"},
            {CardHierarchy.C5, "5"},
            {CardHierarchy.C6, "6"},
            {CardHierarchy.C7, "7"},
            {CardHierarchy.C8, "8"},
            {CardHierarchy.C9, "9"},
            {CardHierarchy.C10, "10"},
            {CardHierarchy.Jack, "J"},
            {CardHierarchy.Queen, "Q"},
            {CardHierarchy.King, "K"},
            {CardHierarchy.Ace, "A"}
        };
        /// <summary>
        /// Возврат нормального вида иерархии карты
        /// </summary>
        /// <param name="h">иерархия карты</param>
        /// <returns>Нормальный вид</returns>
        public static string GetHierarchyNormalView(CardHierarchy h)
        {
            return CardHierarchyNormalView[h];
        }
       
        /// <summary>
        /// Нормальный вид масти карты
        /// </summary>
        static Dictionary<CardSuit, string> CardSuitNormalView =
           new Dictionary<CardSuit, string>()
        {
            {CardSuit.clubs, "♣"},
            {CardSuit.diamonds, "♦"},
            {CardSuit.hearts, "♥"},
            {CardSuit.pikes, "♠"},

        };
        /// <summary>
        /// Возврат нормального вида масти карты
        /// </summary>
        /// <param name="h">масть карты</param>
        /// <returns>Нормальный вид</returns>
        public static string GetSuitNormalView(CardSuit h)
        {
            return CardSuitNormalView[h];
        }

        /// <summary>
        /// количество очков, которые дает карта
        /// </summary>
        static Dictionary<CardHierarchy, int> playingCardValue = new Dictionary<CardHierarchy, int>()
        {
        {CardHierarchy.C2, 2},
        {CardHierarchy.C3, 3},
        {CardHierarchy.C4, 4},
        {CardHierarchy.C5, 5},
        {CardHierarchy.C6, 6},
        {CardHierarchy.C7, 7},
        {CardHierarchy.C8, 8},
        {CardHierarchy.C9, 9},
        {CardHierarchy.C10, 10},
        {CardHierarchy.Jack, 10},
        {CardHierarchy.Queen, 10},
        {CardHierarchy.King, 10}
        };


        /// <summary>
        /// Определяет количество очков, которые дает карта
        /// </summary>
        /// <param name="hierarchy">Иерархия карты</param>
        /// <returns>Количество очков карты конкретной иерархии</returns>
        public static int GetCardValue(CardHierarchy hierarchy)
        {
            return playingCardValue[hierarchy];
        }

        /// <summary>
        /// Тасует колоду с помощью  временной кооды
        /// </summary>
        /// <param name="genDeck">исходная колода</param>
        /// <returns>потасованная колода</returns>
        public static Stack<CardFactory> ShufflingOfTheDeck(Stack<CardFactory> genDeck) 
        {
            //временная колода
            List<CardFactory> tempDeck = new List<CardFactory>();

            //перенос всех карт во временную колу, обнуление начальной
            tempDeck.AddRange(genDeck);
            genDeck.Clear();
            Random R = new Random();
            
            //рандомное распределение карт в начальную колоду
            while (tempDeck.Count>0)
            {
                int i=R.Next(0, tempDeck.Count-1);
                genDeck.Push(tempDeck[i]);
                tempDeck.RemoveAt(i);
            }

            return genDeck;    
        }

        /// <summary>
        /// Находит всех тузов в колоде
        /// </summary>
        /// <param name="cf">Исходная колода</param>
        /// <returns>Коллекцию тузов</returns>
        public static List<TwoPointCard> FindAllAces(Stack<CardFactory> genDeck)
        {
            //создание временного массива, заполнение 
            //его элементами типа TwoPointCard из принимаемой колоды
            List<TwoPointCard> tempAces=new List<TwoPointCard>();
            foreach (CardFactory card in genDeck)
            {
                if (card is TwoPointCard)
                    tempAces.Add(card as TwoPointCard);
            }

            return tempAces;
        }
    }
}
