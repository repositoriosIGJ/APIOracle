using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Repositorio.Contrato
{
    public interface IDestinoTramiteRepository
    {
        ResponseDTO<List<DestinoTramiteDTO>> GetDestinosTramite(DestinoTramite destinoTramite);
        ResponseDTO<List<DestinoTramiteDTO>> GetUltimoDestinoTramite(DestinoTramite destinoTramite);
        ResponseDTO<List<DestinoTramiteDTO>> GetTramiteSinRecibirXDestinoDpto(string destino);
        ResponseDTO<List<DestinoTramiteDTO>> GetTramitesRecibidosXDestinoDepto(DestinoTramiteDTO destinoTramite);
        ResponseDTO<bool> RecibirAsignarSubDestinTramite(DestinoTramite destinoTramite);
    }
}
