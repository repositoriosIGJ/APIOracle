﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class DatosCivilesDTO
    {
        public string Denominacion { get; set; }
        public string Tramite { get; set; }
        public string Destino { get; set; }
        public System.DateTime Pase { get; set; }
        public Nullable<System.DateTime> Registracion { get; set; }
        public System.DateTime Alta { get; set; }
        public int Correlativo { get; set; }
        public string Descripcion { get; set; }
    }
}