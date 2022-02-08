using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionesAPI.Core.Models
{
    public class ziete
    {
        public string ducto { get; set; }
        public double tiempoOperando { get; set; }
        public double diasOperando { get; set; }
        public int vecesOperando { get; set; }
        public double tiempoSuspendidoLogistico { get; set; }
        public int vecesLogistico { get; set; }
        public double tiempoSuspendidoNoLogistico { get; set; }
        public int vecesNoLogistico { get; set; }
        public double tiempoSuspendido { get; set; }
        public double diasSuspendido { get; set; }
        public double porTO { get; set; }
        public double porFO { get; set; }
        public double porSiLog { get; set; }
        public double porNoLog { get; set; }
        public double tiempoTOTAL { get; set; }
    }
}
