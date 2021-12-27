using System;
using System.Collections.Generic;
using System.Text;

namespace suspensionesAPI.Core.Models
{
    public class Suspensiones
    {
        public int id { get; set; }
        public List<Ducto_cat> Ducto_cats { get; set; }
        public List<MotivoSuspension_cat> motivoSuspensions_cats { get; set; }
        public List<PersonalCC_cat> personalCC_cats { get; set; }
        public int estatus { get; set; }
        public DateTime fechaHora { get; set; }
        public string observaciones { get; set; }
        public int kilometro { get; set; }

        public int bph { get; set; }
        public int bls { get; set; }
        public DateTime fechaHoraRegistro { get; set; }
            
    }
}
