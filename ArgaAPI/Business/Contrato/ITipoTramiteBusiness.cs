using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.Data;
using ArgaAPI.Models;

namespace ArgaAPI.Business.Contrato
{
    public interface ITipoTramiteBusiness
    {
        IEnumerable<TABGEN_PROD> GetTipoTramites();
        IEnumerable<TipoTramite> GetTiposTramites();
    }
}
