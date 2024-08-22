using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.Data;
using ArgaAPI.Models;

namespace ArgaAPI.Repositorio.Contrato
{
    public interface ITipoTramiteRepository
    {
        IEnumerable<TABGEN_PROD> GetTipoTramites();
        IEnumerable<TipoTramite> GetTiposTramites();
    }
}
