using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Repositorio.Contrato;
using ArgaAPI.Data ;
using Newtonsoft.Json;
using ArgaAPI.Models;

using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class TipoSocietarioRepository : ITipoSocietarioReposity
    {



        #region Miembros de ITipoSocietarioReposity




        public IEnumerable<TipoSocietario> GetTiposSocietarios()
        {
            List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();
            using (var context = new Entities())
            {
                const string sql = " select * from tabgen t where t.tabtipotab = 002 and t.tabclave != '*' order by t.tabclave asc";

                var ListaTipoSocietarioDB = context.Database.SqlQuery<TABGEN_PROD>(sql).ToList();



                foreach (var tipoSocietariodb in ListaTipoSocietarioDB)
                {

                    TipoSocietario tiposocietario = MapToTipoSocietario(tipoSocietariodb);
                    ListaTiposSocietarios.Add(tiposocietario);
                }
                return ListaTiposSocietarios;
            }
        }

        public TipoSocietario MapToTipoSocietario(TABGEN_PROD tabgenProd)
        {

            TipoSocietario tiposocietario = new TipoSocietario()
            {

                Codigo = tabgenProd.TABCLAVE,
                Nombre = tabgenProd.TABCONTEN1,
                Acronimo = tabgenProd.TABCONTEN2

            };

            return tiposocietario;
        }
    }
}

#endregion
