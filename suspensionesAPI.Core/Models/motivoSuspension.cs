using suspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SuspensionesAPI.Core.Models
{
    public class cat_motivoSuspension
    {
       
        public int id { get; set; }
        public string nombre { get; set; }
        public int logisticaid { get; set; }
        public cat_logistica logistica { get; set; }
     
    }
}

