using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace suspensionesAPI.Core.Models
{
    public class cat_ducto
    {
        public int id { get; set; }
        [Required(ErrorMessage = "EL nombre del {0} es obligatorio")]
        public string nombre { get; set; }

        public int estatus { get; set; }

        //public List<suspensiones> suspension { get; set; }
    }
}
