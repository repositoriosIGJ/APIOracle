using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Data;
using ArgaAPI.DTOs;
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
        public IEnumerable<TipoSocietario> GetTiposSocietarios()
        {

            var tiposSocietarios = _tipoSocietarioBusiness.GetTiposSocietarios();

            return tiposSocietarios;
        }

        public List<TipoSocietario> GetCodigosSinCeroALaIzq()
        {

            var tiposSocietarios = _tipoSocietarioBusiness.GetTiposSocietariosCodigosSinCeroALaIzq().ToList();

            return tiposSocietarios;
        }

        // GET api/TipoSocietario/5

       
        //GET api/tiposocietario/GetByCodigoSocietario?codigo={codigo}
        public TipoSocietario GetByCodigoTipoSocietario(string codigo)
        {
            var tipoSocietario = _tipoSocietarioBusiness.GetTipoSocietarioPorCodigo(codigo);
            return tipoSocietario;
        }

        // GET api/TipoSocietario/GetByTipo?tipo={tipo}
        public IEnumerable<TipoSocietario> GetByTipo(string tipo)
        {
            var tipoSocietario = _tipoSocietarioBusiness.GetTipoSocietarioPorTipo(tipo);
            return tipoSocietario;
        }

        // POST api/TipoSocietario
        /*public void Post([FromBody]string value)
        {
        }
        public ResponseDTO<bool> Post([FromBody] TipoSocietario tipoSocietario)
        {
            var rsp = _tipoSocietarioBusiness.Insert(tipoSocietario);
            return rsp;
        }
        */
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
