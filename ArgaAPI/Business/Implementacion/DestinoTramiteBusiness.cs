using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Business.Contrato;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;

namespace ArgaAPI.Business.Implementacion
{
    public class DestinoTramiteBusiness : IDestinoTramiteBusiness
    {
        private readonly IDestinoTramiteRepository _destinoTramiteRepository;
        public DestinoTramiteBusiness(IDestinoTramiteRepository destinoTramiteRepository)
        {
            _destinoTramiteRepository = destinoTramiteRepository;
        }


        #region Miembros de IDestinoTramiteBusiness

        public ResponseDTO<List<DestinoTramiteDTO>> GetDestinosTramite(DestinoTramite destinoTramite)
        {
            var rst = _destinoTramiteRepository.GetDestinosTramite(destinoTramite);

            return rst;
        }

        #endregion

        #region Miembros de IDestinoTramiteBusiness


        public ResponseDTO<List<DestinoTramiteDTO>> GetUltimoDestinoTramite(DestinoTramite destinoTramite)
        {
            var rst = _destinoTramiteRepository.GetUltimoDestinoTramite(destinoTramite);

            return rst;
        }

        #endregion

        #region Miembros de IDestinoTramiteBusiness


        public ResponseDTO<List<DestinoTramiteDTO>> GetTramiteSinAsignarXDestinoDpto(string destino)
        {
            var rst = _destinoTramiteRepository.GetTramiteSinRecibirXDestinoDpto(destino);

            return rst;
        }

        #endregion

        #region Miembros de IDestinoTramiteBusiness


        public ResponseDTO<List<DestinoTramiteDTO>> GetTramitesRecibidosXDestinoDepto(DestinoTramiteDTO destinoTramite)
        {
            var rst = _destinoTramiteRepository.GetTramitesRecibidosXDestinoDepto(destinoTramite);

            return rst;
        }

        #endregion

        #region Miembros de IDestinoTramiteBusiness


        public ResponseDTO<bool> RecibirAsignarSubDestinTramite(DestinoTramite destinoTramite)
        {
            var rst = _destinoTramiteRepository.RecibirAsignarSubDestinTramite(destinoTramite);

            return rst;

        #endregion
        }
    }
}