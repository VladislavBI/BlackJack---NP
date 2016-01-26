using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackServer
{
    public static class DelegatesData
    {
       

        #region Online workers
        /// <summary>
        /// Измение вида окна сервера при изменении его статуса
        /// </summary>
        /// <param name="b">Статус сервера</param>
        public delegate void ServerStatusChangeView(int i);
        /// <summary>
        /// Экземляр делегата ServerStatusChangeView: 
        /// измение вида окна сервера при изменении его статуса
        /// </summary>
        public static ServerStatusChangeView HandlerServerStatusChangeView;
        #endregion
    }
}
