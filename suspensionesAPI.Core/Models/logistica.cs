using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace suspensionesAPI.Core.Models
{
    public class cat_logistica
    {
        public int id { get; set; }
        [Required(ErrorMessage = "EL campo {0} es obligatorio")]
        public string nombre { get; set; }
    }
}
