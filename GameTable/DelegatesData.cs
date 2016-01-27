using GameTable.OnlineGame;
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


        /// <summary>
        /// Игрок заканчивает ход - делегат
        /// </summary>
        public delegate void PlayerIsMoreThanEnough();
        /// <summary>
        /// Экземляр делегата PlayerIsMoreThanEnough: 
        /// игрок заканчивает ход
        /// </summary>
        public static PlayerIsMoreThanEnough HandlerPlayerIsMoreThanEnough;

        /// <summary>
        /// Создание внешнего вида стола для игрока - делегат
        /// </summary>
        public delegate void CreateTableViewForCurrentPlayer();
        /// <summary>
        /// Экземляр делегата CreateTableViewForCurrentPlayer: 
        /// создание внешнего вида стола для игрока
        /// </summary>
        public static CreateTableViewForCurrentPlayer HandlerCreateTableViewForCurrentPlayer;

        /// <summary>
        /// Показать победителя - делегат
        /// </summary>
        public delegate void WinnerPlayerShow();
        /// <summary>
        /// Экземляр делегата WinnerPlayerShow: 
        /// показать победителя
        /// </summary>
        public static WinnerPlayerShow HandlerWinnerPlayerShow;

        /// <summary>
        /// Изменение состояния кнопок взять карту/пасс - делегат
        /// </summary>
        /// <param name="b">Игра идет?</param>
        public delegate void TableButtonsIsEnanbleChange(bool b);
        /// <summary>
        /// Экземляр делегата TableButtonsIsEnanbleChange: 
        /// изменение состояния кнопок взять карту/пасс
        /// </summary>
        public static TableButtonsIsEnanbleChange HandlerTableButtonsIsEnanbleChange;


        public delegate void PlayersListRefresh(SendingData s);
        public static PlayersListRefresh HandlerPlayersListRefresh;

        public delegate void GameTableOpen();
        public static GameTableOpen HandlerGameTableOpen;

        public delegate void GameTableStatisticTB(string s);
        public static GameTableStatisticTB HandlerGameTableStatisticTB;
    }
}
