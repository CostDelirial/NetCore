using suspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionesAPI.Core.Models
{
    public class suspensiones
    {
        public int id { get; set; }
        public string estatus { get; set; }
        public DateTime fechaHora { get; set; }
        public int duracion { get; set; }
        public string observaciones { get; set; }
        public double km { get; set; }
        public int bph { get; set; }
        public int bls { get; set; }
        public DateTime seregistro { get; set; }
//----------------------------------------------------------------------------------------------------------
//                                      LLAVES FORANEAS
//----------------------------------------------------------------------------------------------------------
        public int ductoId { get; set; }
        public int motivoSuspensionId { get; set; }
        public int personalCCId { get; set; }
//----------------------------------------------------------------------------------------------------------
//                                      TABLAS RELACIONADAS
//----------------------------------------------------------------------------------------------------------
        public cat_ducto ducto { get; set; }
        public cat_motivoSuspension motivoSuspension { get; set; }
        public cat_personalCC personalCC { get; set; }
        
    }
}
