﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class DatosCiviles
    {
        public string TRACODTRAM { get; set; }
        public System.DateTime TRAFECHACT { get; set; }
        public int TRANROCORR { get; set; }
        public Nullable<int> TRANROTRAM { get; set; }
        public Nullable<System.DateTime> TRAFECREG { get; set; }

        public string EXPRAZONSO { get; set; }
        public short EXPTIPOSOC { get; set; }

        public string DTRCODDEST { get; set; }
        public System.DateTime DTRFECHART { get; set; }

        public string CDTDESCTRAM { get; set; }


    }
}