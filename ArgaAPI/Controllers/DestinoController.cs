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
    public class DestinoController : ApiController
    {
        private readonly IDestinoBusiness _destinobusiness;

        public DestinoController(IDestinoBusiness destinobusiness)
        {
            _destinobusiness = destinobusiness;
        }
        // GET api/destino/GetDestinos
        [HttpGet]
        [ActionName("GetDestinos")]
        public ResponseDTO<List<Destino>> GetDestinos()
        {
           var rst = _destinobusiness.GetDestinos();
           return rst;
        }

      
    }
}
