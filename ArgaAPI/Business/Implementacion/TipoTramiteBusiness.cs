using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Data;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;

namespace ArgaAPI.Business.Implementacion
{
    public class TipoTramiteBusiness : ITipoTramiteBusiness
    {
        private readonly  ITipoTramiteRepository _tipoTramiteRepository ;

        public TipoTramiteBusiness(ITipoTramiteRepository tipoTramiteRepository) {

            _tipoTramiteRepository = tipoTramiteRepository;
        }
        #region Miembros de ITipoTramiteBusiness


        #endregion

        #region Miembros de ITipoTramiteBusiness


        public IEnumerable<TipoTramite> GetTiposTramites()
        {
            var tipotramites = _tipoTramiteRepository.GetTiposTramites();

            return tipotramites;
        }

        #endregion

        #region Miembros de ITipoTramiteBusiness


        public ResponseDTO<TipoTramite> GetTramitesbyCodigoTramite(string codigo)
        {
            var rsp =_tipoTramiteRepository.GetTramitesbyCodigoTramite(codigo);

            return rsp;
        }

        #endregion
    }
}