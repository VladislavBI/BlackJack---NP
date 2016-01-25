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



        #region Online workers
        /// <summary>
        /// Измение вида окна сервера при изменении его статуса
        /// </summary>
        /// <param name="b">Сервер работает?</param>
        public delegate void ServerStatusChangeView(bool b);
        /// <summary>
        /// Экземляр делегата ServerStatusChangeView: 
        /// измение вида окна сервера при изменении его статуса
        /// </summary>
        public static ServerStatusChangeView HandlerServerStatusChangeView;
        #endregion
    }
}
