using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Data;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Controllers
{
    public class TipoTramiteController : ApiController
    {
        private readonly ITipoTramiteBusiness _tipoTramiteBusiness;

        public TipoTramiteController(ITipoTramiteBusiness tipoTramiteBusiness)
        {
            _tipoTramiteBusiness = tipoTramiteBusiness;
        }

        [HttpGet]
        public IEnumerable<TipoTramite> GetAllTipoTramites()
        {
            var tipostramites = _tipoTramiteBusiness.GetTiposTramites();
            return tipostramites;
        }

        [HttpGet]
        public ResponseDTO<TipoTramite> GetTipoTramitebyCodigo(string codigo)
        {
            var tipotramite = _tipoTramiteBusiness.GetTramitesbyCodigoTramite(codigo);
            return tipotramite;
        }
    
    }
}
