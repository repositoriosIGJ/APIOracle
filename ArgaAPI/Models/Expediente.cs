using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class Expediente
    {
        public int Correlativo { get; set; }

        public string RazonSocial { get; set; }

        public int Referencial { get; set; }

        public int CodigoTipoSocietario { get; set; }

        public string TipoSocietario { get; set; }

        public long? Cuil { get; set; }


    }
}