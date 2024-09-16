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
    public class TramiteBusiness : ITramiteBusiness
    {
        private readonly ITramiteRepository _tramiteRepository;
        public TramiteBusiness(ITramiteRepository tramiteRepository)
        {
            _tramiteRepository = tramiteRepository;
        }

        #region Miembros de ITramiteBusiness

        public ResponseDTO<List<Tramite>> GetTramites(Tramite tramite)
        {
             var rsp = _tramiteRepository.GetTramites(tramite);

             return rsp;
           
        }

       

        #endregion
    }
}