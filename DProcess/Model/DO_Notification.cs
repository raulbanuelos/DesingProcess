using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DO_Notification
    {
        public int ID_NOTIFICACION { get; set; }
        public string ID_USUARIO_SEND { get; set; }
        public string ID_USUARIO_RECEIVER { get; set; }
        public string TITLE { get; set; }
        public string MSG { get; set; }

        /// <summary>
        /// 1:Success, 2:Warning, 3:Error, 0: Information
        /// </summary>
        public int TYPE_NOTIFICATION { get; set; }
    }
}
