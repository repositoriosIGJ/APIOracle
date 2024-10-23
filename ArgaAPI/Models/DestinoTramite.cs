using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class DestinoTramite
    {
        public int Correlativo { get; set; }

        public string CodigoTramite { get; set; }

        public DateTime? FechaComienzoTramite { get; set; }

        public int? Numerotramite { get; set; }

        public string CodigoDestino { get; set; }

        public string UsuarioDestino { get; set; }

        public DateTime? FechaIngresoDestino { get; set; }

        public DateTime? FechaSalidaDestino { get; set; }

        public string DestinoAnterior { get; set; }

        public int? NroSubdestino { get; set; }





    }
    }
