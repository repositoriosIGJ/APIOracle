using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Repositorio.Implementacion
{
    public interface IUserDestinoRepository
    {
        ResponseDTO<List<UserDestino>> GetDestinosUser(string username);
    }
}
