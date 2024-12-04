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
    public class DestinoTramiteController : ApiController
    {
         private readonly IDestinoTramiteBusiness _destinoTramitebusiness;

         public DestinoTramiteController(IDestinoTramiteBusiness destinoTramitebusiness)
        {
            _destinoTramitebusiness = destinoTramitebusiness;
        }
         // GET api/destino/GetDestinosTramite
        [HttpPost]
         [ActionName("GetDestinosTramite")]
         public ResponseDTO<List<DestinoTramite>> GetDestinos(DestinoTramite destinoTramite)
         {
             var rst = _destinoTramitebusiness.GetDestinosTramite(destinoTramite);
             return rst;
         }

        // GET api/destino/GetUltimoDestinoTramite
        [HttpPost]
        [ActionName("GetUltimoDestinoTramite")]
        public ResponseDTO<List<DestinoTramite>> GetUltimoDestino( DestinoTramite destinoTramite)
        {
            var rst = _destinoTramitebusiness.GetUltimoDestinoTramite(destinoTramite);
            return rst;
        }

        [HttpGet]
        [ActionName("GetTramiteSinAsignarXDestinoDpto")]
        public ResponseDTO<List<DestinoTramiteDTO>> GetTramiteSinAsignarXDestinoDpto(string destino)
        {
            var rst = _destinoTramitebusiness.GetTramiteSinAsignarXDestinoDpto(destino);

            return rst;
        }

        // GET api/destino/GetTramitesRecibidosXDestinoDepartamento
        [HttpPost]
        [ActionName("GetTramitesRecibidosXDestinoDepartamento")]
        public ResponseDTO<List<DestinoTramiteDTO>> GetTramitesRecibidosXDestinoDepartamento(DestinoTramiteDTO destinoTramite)
        {
            var rst = _destinoTramitebusiness.GetTramitesRecibidosXDestinoDepto(destinoTramite);
            return rst;
        }
    }
}
