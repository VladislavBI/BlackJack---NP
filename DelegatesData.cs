using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTable
{
    public static class DelegatesData
    {
        /// <summary>
        /// Взять карту из общей колоды - делегат
        /// </summary>
        /// <returns>Карта</returns>
        public delegate CardDeck.CardFactory GetingCardFromGenDeck();

        /// <summary>
        /// Экземляр делегата GetingCardFromGenDeck: 
        /// взять карту из общей колоды
        /// </summary>
        public static GetingCardFromGenDeck HandlerGetingCardFromGenDeck;
    }
}
