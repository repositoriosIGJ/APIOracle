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
    public class SubdestinoBusiness : ISubdestinoBusiness
    {
        private readonly ISubdestinoRepository _subdestinoRepository;

       

        public SubdestinoBusiness(ISubdestinoRepository subdestinoRepository)
        {
            _subdestinoRepository = subdestinoRepository;
        }
    
        #region Miembros de ISubdestinoBusiness

        public ResponseDTO<List<Subdestino>> GetAgentesXSubdestino(string subdestino)
        {
            var rst = _subdestinoRepository.GetAgentesXSubdestino(subdestino);
         
            return rst;
        }

        #endregion
    }
}