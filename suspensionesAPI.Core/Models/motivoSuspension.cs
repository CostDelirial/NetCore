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
       [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "EL nombre del {0} es obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una {1}")]
        public int logisticaid { get; set; }
        public cat_logistica logistica { get; set; }


        //
        //public List<suspensiones> suspension { get; set; }

    }
}

