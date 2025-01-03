﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.DTOs
{
    public class DestinoTramiteDTO
    {
        // NUEVAS PROP DE INNER JOINS
        public string RazonSocial { get; set; }
        
        public string TipoSocietario { get; set; }

        public string CodigoSocietario { get; set; }

        public string DescripcionTramite { get; set; }

        public string UrgenteNormal { get; set; }

        public string RegistralInfoContable { get; set; }

        public string DestinoAnteriordDpto { get; set; }

        public string DestinoAnteriordArea { get; set; }

        public string AreaDestinoActual { get; set; }

        public string DepartamentoDestinoActual { get; set; }

        public string NombreUsuarioRecepciona { get; set; }

        public string UserRecepciona { get; set; }

        public string NombreUsuarioAsigando { get; set; }

        public string UserUsuarioAsignado { get; set; }

        public string Destino { get; set; }

        // FIN 
        public int Correlativo { get; set; }

        public string CodigoTramite { get; set; }

        public string FechaComienzoTramite { get; set; }

        public int? Numerotramite { get; set; }

        public string CodigoDestino { get; set; }

        public string UsuarioDestino { get; set; }

        public string FechaIngresoDestino { get; set; }

        public string FechaSalidaDestino { get; set; }

        public string DestinoAnterior { get; set; }

        public int? NroSubdestino { get; set; }
        
        public string DestinoHacia { get; set; }
        
    }
}