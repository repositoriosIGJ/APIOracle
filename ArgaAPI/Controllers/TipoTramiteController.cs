using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Data;
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
        // GET api/tipotramite
        /*public IEnumerable<TABGEN_PROD> Get()
        {
            var tipotramites = _tipoTramiteBusiness.GetTipoTramites();
            return tipotramites;
        }*/

        public IEnumerable<TipoTramite> Get()
        {
            var tipostramites = _tipoTramiteBusiness.GetTiposTramites();
            return tipostramites;
        }

        // GET api/tipotramite/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tipotramite
        public void Post([FromBody]string value)
        {
        }

        // PUT api/tipotramite/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tipotramite/5
        public void Delete(int id)
        {
        }
    }
}
