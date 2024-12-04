using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Business.Contrato
{
    public interface IDestinoTramiteBusiness
    {
        ResponseDTO<List<DestinoTramite>> GetDestinosTramite(DestinoTramite destinoTramite);
        ResponseDTO<List<DestinoTramite>> GetUltimoDestinoTramite(DestinoTramite destinoTramite);
        ResponseDTO<List<DestinoTramiteDTO>> GetTramiteSinAsignarXDestinoDpto(string destino);
        ResponseDTO<List<DestinoTramiteDTO>> GetTramitesRecibidosXDestinoDepto(DestinoTramiteDTO destinoTramite);
    }
}
