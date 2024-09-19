﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class DatosCivilesRequest
    {
        public int Tipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}