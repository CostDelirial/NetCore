using suspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionesAPI.Core.Models
{
    public class cat_motivoSuspension
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public List<cat_logistica> logisticaId { get; set; }
    }
}
