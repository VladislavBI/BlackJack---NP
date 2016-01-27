using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTable.CardDeck
{
    /// <summary>
    /// Класс карты для отправки
    /// </summary>
    public class ShortCard
    {
        public ShortCard()
        { }
        public ShortCard(CardHierarchy hierarchy, CardSuit suit)
        {
            thisCardHierarchy = Convert.ToInt32(hierarchy);
            thisCardSuit = Convert.ToInt32(suit);
        }
        /// <summary>
        /// иерархия конкретной карты
        /// </summary>
        public int thisCardHierarchy { get; set; }
        /// <summary>
        /// масть карты
        /// </summary>
        public int thisCardSuit { get; set; }

        public CardHierarchy GetHierarchy()
        {
            return (CardHierarchy)thisCardHierarchy;
        }
        public CardSuit GetSuit()
        {
            return (CardSuit)thisCardSuit;
        }
    }
    
    /// <summary>
    /// Абстрактный класс для создания карты
    /// </summary>
    public class CardFactory
    {
       
        /// <summary>
        /// иерархия конкретной карты
        /// </summary>
        public CardHierarchy thisCardHierarchy { get; protected set; }
        /// <summary>
        /// масть карты
        /// </summary>
        public CardSuit thisCardSuit { get; protected set; }
        
#region Возвращение очков карт

        /// <summary>
        /// Возврат очков для карты, не являющейся тузом. 
        /// Для туза - исключение
        /// </summary>
        /// <returns>Количество очков</returns>
        public virtual int GetNonAceCardPoints()
        {
            throw new Exception(); 
        }

        /// <summary>
        ///  Возврат очков для туза.
        ///  Для карты, не являющейся тузом - исключение
        /// </summary>
        /// <returns>Количество очков</returns>
        public virtual int[] GetAcePointsCard()
        {
            throw new Exception(); 
        }
        
#endregion

        public CardFactory (CardHierarchy hierarchy, CardSuit suit)
	    {
            thisCardHierarchy=hierarchy;
            thisCardSuit=suit;
	    }

        public override string ToString()
        {
            return CardGeneralInfoCreator.GetSuitNormalView(thisCardSuit) +
                CardGeneralInfoCreator.GetHierarchyNormalView(thisCardHierarchy);
        }
    }

    /// <summary>
    /// Создание карты с 1им вариантом очков - не туз
    /// </summary>
    public class OnePointCard : CardFactory
    {
        /// <summary>
        /// Ценность карты- то сколько очков она даёт
        /// </summary>
        public int cardValue { get; set; }

        /// <summary>
        /// Возврат очков карты-не туза
        /// </summary>
        /// <returns>количество очков</returns>
        public override int GetNonAceCardPoints()
        {
 	         return cardValue;
        } 

        public OnePointCard (CardHierarchy hierarchy, CardSuit suit):base(hierarchy,suit)
	    {
            cardValue=CardGeneralInfoCreator.GetCardValue(hierarchy);
	    }
    }

    /// <summary>
    /// Создание карты с 2я вариантами очков - туз
    /// </summary>
    public class TwoPointCard:CardFactory
    {
        /// <summary>
        /// Ценность карты- то сколько очков она даёт
        /// </summary>
        public int[] cardValue = new int[2];
        
        /// <summary>
        /// Возврат очков карты-туза
        /// </summary>
        /// <returns>количество очков</returns>
        public override int[] GetAcePointsCard()
        {
 	         return cardValue;
        } 
        public TwoPointCard(CardHierarchy hierarchy, CardSuit suit):base(hierarchy,suit)
	    {
            cardValue[0]=1;
            cardValue[1]=11;
        }
    }

}
