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
    public class ExpedienteController : ApiController
    {

        private readonly IExpedienteBusiness _expedienteBusiness;

        public ExpedienteController(IExpedienteBusiness expedienteBusiness)
        {
            _expedienteBusiness = expedienteBusiness;
        }
       

        

       /* [HttpGet]
        [ActionName("GetExpedientes")]
        public ResponseDTO<List<Expediente>> GetExpedientes([FromBody] Expediente expediente)
        {
            var rsp = _expedienteBusiness.GetExpedientes( expediente);
            return rsp;
        }*/

        [HttpPost]
        [ActionName("GetExpedientes")]
        public ResponseDTO<List<Expediente>> GetExpedientes([FromBody] Expediente expediente)
        {
            var rsp = _expedienteBusiness.GetExpedientes(expediente);
            return rsp;
        }


        
    }
}
