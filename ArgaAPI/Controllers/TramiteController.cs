using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ArgaAPI.Business.Contrato;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Controllers
{
    public class TramiteController : ApiController
    {
        private readonly ITramiteBusiness _tramitebusiness;
        public TramiteController(ITramiteBusiness tramitebusiness)
        {
            _tramitebusiness = tramitebusiness;
        }

        // GET api/tramite/GetTramites

        [HttpPost]
        [ActionName("GetTramites")]
        public ResponseDTO<List<Tramite>> GetTramites(Tramite tramite)
        {
            var rsp = _tramitebusiness.GetTramites(tramite);

            return rsp;
        }

 

     

     

     
    }
}
