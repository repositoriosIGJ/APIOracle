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
    public class SubdestinoController : ApiController
    {
        private readonly ISubdestinoBusiness _subdestinobusiness;

        public SubdestinoController(ISubdestinoBusiness subdestinobusiness)
        {
            _subdestinobusiness = subdestinobusiness;
        }

        [HttpGet]
        [ActionName("GetAgentesXSubdestino")]
        public ResponseDTO<List<Subdestino>> GetAgentesXSubdestino(string subdestino)
        {
            var rst = _subdestinobusiness.GetAgentesXSubdestino(subdestino);

            return rst;
        }
    }
}
