using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Business.Contrato
{
    public interface ISubdestinoBusiness
    {
        ResponseDTO<List<Subdestino>> GetAgentesXSubdestino(string subdestino);
    }
}
