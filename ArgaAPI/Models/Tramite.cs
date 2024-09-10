using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class Tramite
    {
        public int? NroTramite { get; set; }
        
        public string CodigoTramite { get; set; }

        public string TipoTramite { get; set; }

        public int Correlativo { get; set; }

        public DateTime? FechaRegistracion { get; set; }




    }
}