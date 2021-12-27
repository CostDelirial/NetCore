using System;
using System.Collections.Generic;
using System.Text;

namespace suspensionesAPI.Core.Models
{
    public class MotivoSuspension_cat
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<Logistica_cat> Logistica_cats { get; set; }

     }
}
