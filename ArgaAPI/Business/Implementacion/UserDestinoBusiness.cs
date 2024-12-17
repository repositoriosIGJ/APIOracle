using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Business.Contrato;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Implementacion;

namespace ArgaAPI.Business.Implementacion
{
    public class UserDestinoBusiness : IUserDestinoBusiness
    {
        #region Miembros de IUserDestinoBusiness

        private readonly IUserDestinoRepository _usuarioDestinoRepository;

        public UserDestinoBusiness(IUserDestinoRepository usuarioDestinoRepository)
        {
            _usuarioDestinoRepository = usuarioDestinoRepository;
        }

        public ResponseDTO<List<UserDestino>> GetDestinosUser(string username)
        {
            var rst = _usuarioDestinoRepository.GetDestinosUser(username);

            return rst;
        }

        #endregion
    }
}