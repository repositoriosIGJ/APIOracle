using System;
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
    public class TipoSocietarioController : ApiController
    {

        private readonly ITipoSocietarioBusiness _tipoSocietarioBusiness;

        public TipoSocietarioController(ITipoSocietarioBusiness tipoSocietarioBusiness)
        {
            _tipoSocietarioBusiness = tipoSocietarioBusiness;
        }
        // GET api/sociedad
        // GET api/TipoSocietario
       /* public IEnumerable<TABGEN_PROD> GetTipoSocietario()
        {
            using (var context = new Entities())
            {
                const string sql = " select * from tabgen t where t.tabtipotab = 002 and t.tabclave != '*'";

                

                var TipoSocietario = context.Database.SqlQuery<TABGEN_PROD>(sql).ToList();

              var Serializado =  JsonConvert.SerializeObject(TipoSocietario);

              var JsonTipoSocietario = JsonConvert.DeserializeObject<IEnumerable<TABGEN_PROD>>(Serializado);





              return TipoSocietario;
            }
        }*/

      
        /*public IEnumerable<TABGEN_PROD> GetTiposSocietarios()
        {

            var tiposSocietarios = _tipoSocietarioBusiness.GetTipoSocietario();

            return tiposSocietarios;
        }*/

        public IEnumerable<TipoSocietario> GetTiposSocietarios()
        {

            var tiposSocietarios = _tipoSocietarioBusiness.GetTiposSocietarios();

            return tiposSocietarios;
        }

        // GET api/TipoSocietario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/TipoSocietario
        public void Post([FromBody]string value)
        {
        }

        // PUT api/TipoSocietario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/TipoSocietario/5
        public void Delete(int id)
        {
        }
    }
}
