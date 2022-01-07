
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace suspensionesAPI.Core.Models
{
    public class cat_logistica
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "EL nombre del {0} es obligatorio")]
        public string nombre { get; set; }

 
        public List<cat_motivoSuspension> cat_motivoSuspension { get; set; }
    }
}
