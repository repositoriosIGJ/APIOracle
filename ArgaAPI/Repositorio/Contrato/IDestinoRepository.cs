using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Repositorio.Contrato
{
    public interface IDestinoRepository
    {
        ResponseDTO<List<Destino>> GetDestinos();
    }
}
