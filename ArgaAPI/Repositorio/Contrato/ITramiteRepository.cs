using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Repositorio.Contrato
{
    public interface ITramiteRepository
    {
        ResponseDTO<List<Tramite>> GetTramites(Tramite tramite);
        ResponseDTO<List<Tramite>> GetTramitesbyCorrelativo(int Correlativo);
       
       
    }
}
