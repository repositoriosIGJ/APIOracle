using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgaAPI.Data;
using ArgaAPI.DTOs;
using ArgaAPI.Models;

namespace ArgaAPI.Business.Contrato
{
 


     public interface ITipoSocietarioBusiness
    {
         
         IEnumerable<TipoSocietario> GetTiposSocietarios();
         IEnumerable<TipoSocietario> GetTiposSocietariosCodigosSinCeroALaIzq();
         TipoSocietario GetTipoSocietarioPorCodigo(string codigo);
         IEnumerable<TipoSocietario> GetTipoSocietarioPorTipo(string tipo);
         // ResponseDTO<bool> Insert(TipoSocietario tipoSocietario);
    }
}
