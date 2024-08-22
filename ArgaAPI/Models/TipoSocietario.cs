using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArgaAPI.Models
{
    public class TipoSocietario
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Acronimo { get; set; }
        public string AcronimoNoMbre
        {
            get { return "["+ Acronimo+"] " + Nombre; }

        }
    }

}