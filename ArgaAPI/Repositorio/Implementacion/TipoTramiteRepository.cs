using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Data;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class TipoTramiteRepository : ITipoTramiteRepository
    {
        #region Miembros de ITipoTramiteRepository

        public IEnumerable<TABGEN_PROD> GetTipoTramites()
        {
            using (var context = new Entities())
            {
                const string sql = " select * from tabgen t where t.tabtipotab = 001 and t.tabclave != '*'";

                var ListaTipoTramitesDB = context.Database.SqlQuery<TABGEN_PROD>(sql).ToList();

             
                  return ListaTipoTramitesDB;
            
                }
              
            }

        #region Miembros de ITipoTramiteRepository


        public IEnumerable<TipoTramite> GetTiposTramites()
        {
          List<TipoTramite> ListaTiposSocietarios = new List<TipoTramite>();
             using (var context = new Entities())
            {
            const string sql = " select * from tabgen t where t.tabtipotab = 001 and t.tabclave != '*'";

            var ListaTipoSocietarioDB = context.Database.SqlQuery<TABGEN_PROD>(sql).ToList();

     

            foreach(var tipoSocietariodb in ListaTipoSocietarioDB){

               var tiposocietario =  MapToTipoSocietario(tipoSocietariodb);
               ListaTiposSocietarios.Add(tiposocietario);
            }
            return ListaTiposSocietarios;
    }
}

        public TipoTramite MapToTipoSocietario(TABGEN_PROD tabgenProd)
        {

            TipoTramite tipoTramite = new TipoTramite()
            {

                Codigo = tabgenProd.TABCLAVE,
                Nombre = tabgenProd.TABCONTEN1

            };

            return tipoTramite;
        }
        #endregion
    }

        #endregion
    }
