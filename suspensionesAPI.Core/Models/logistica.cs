
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace suspensionesAPI.Core.Models
{
    public class cat_logistica
    {
        
        public int id { get; set; }
        
        public string nombre { get; set; }

        public List<cat_motivoSuspension> motivoSuspensiones { get; set; }
    }
}
