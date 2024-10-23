﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Data;
using ArgaAPI.Models;
using Newtonsoft.Json;

namespace ArgaAPI.Controllers
{
    public class DatosCivilesController : ApiController
    {
        private readonly IDatosCivilesBusiness _DatosCivilesBusiness;

        public DatosCivilesController(IDatosCivilesBusiness DatosCivilesBusiness)
        {
            _DatosCivilesBusiness = DatosCivilesBusiness;
        }

        [HttpPost]
        public IEnumerable<DatosCivilesDTO> GetDatosCiviles(DatosCivilesRequest datosCivilesReq)
        {

            var datosCiviles = _DatosCivilesBusiness.GetDatosCiviles(datosCivilesReq);

            return datosCiviles;
        }
    }
}