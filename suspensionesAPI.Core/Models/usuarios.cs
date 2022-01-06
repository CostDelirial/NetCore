using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionesAPI.Core.Models
{
    public class usuarios
    {
        public int id { get; set; }
        public int control { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public int tipo { get; set; }
        public int estatus { get; set; }
        //public logs Logs { get; set; }

    }
}
