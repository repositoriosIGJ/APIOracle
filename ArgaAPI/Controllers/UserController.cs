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
    public class UserController : ApiController
    {
        
        private readonly IUserDestinoBusiness _usuarioDestinoBusiness;

        public UserController(IUserDestinoBusiness usuarioDestinoBusiness)
        {
            _usuarioDestinoBusiness = usuarioDestinoBusiness;
        }

        [HttpGet]
        [ActionName("GetDestinosUser")]
        public ResponseDTO<List<UserDestino>> GetDestinosUser(string username)
        {
            var rst = _usuarioDestinoBusiness.GetDestinosUser(username);

            return rst;
        }
    }
}
