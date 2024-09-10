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
    public class ExpedienteBusiness : IExpedienteBusiness 
    {

        private readonly IExpedienteRepository _expedienteRepository;

        public ExpedienteBusiness(IExpedienteRepository expedienteRepository)
        {
            _expedienteRepository = expedienteRepository;
        }

      

       

        

        

        #region Miembros de IExpedienteBusiness


        public ResponseDTO<List<Expediente>> GetExpedientes(Expediente expediente)
        {
            var rst = _expedienteRepository.GetExpedientes(expediente);

           return rst;
        }

        #endregion
    }
}