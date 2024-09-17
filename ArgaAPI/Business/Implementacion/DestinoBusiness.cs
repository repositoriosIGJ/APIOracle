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
    public class DestinoBusiness : IDestinoBusiness
    {
        private readonly IDestinoRepository _destinoRepository;

        public DestinoBusiness(IDestinoRepository destinoRepository)
        {
            _destinoRepository = destinoRepository;
        }
        #region Miembros de IDestinoBusiness

        public ResponseDTO<List<Destino>> GetDestinos()
        {
          var rst =  _destinoRepository.GetDestinos();

          return rst;
        }

        #endregion
    }
}