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

      
        // GET api/TipoSocietario/GetTiposSocietarios
        public ResponseDTO<IEnumerable<TipoSocietario>> GetTiposSocietarios()
        {

            var tiposSocietarios = _tipoSocietarioBusiness.GetTiposSocietarios();

            return tiposSocietarios;
        }

        public ResponseDTO<IEnumerable<TipoSocietario>> GetCodigosSinCeroALaIzq()
        {
            var rst =_tipoSocietarioBusiness.GetTiposSocietariosCodigosSinCeroALaIzq();
           
            return rst;
        }

  

       
        //GET api/tiposocietario/GetByCodigoSocietario?codigo={codigo}
        public ResponseDTO<TipoSocietario> GetByCodigoTipoSocietario(string codigo)
        {
            var tipoSocietario = _tipoSocietarioBusiness.GetTipoSocietarioPorCodigo(codigo);
            return tipoSocietario;
        }

        // GET api/TipoSocietario/GetByTipo?tipo={tipo}
        public ResponseDTO<IEnumerable<TipoSocietario>> GetByTipo(string tipo)
        {
            var tipoSocietario = _tipoSocietarioBusiness.GetTipoSocietarioPorTipo(tipo);
            return tipoSocietario;
        }

      
       
    }
}
