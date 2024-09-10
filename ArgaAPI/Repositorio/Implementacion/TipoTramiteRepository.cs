using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArgaAPI.Data;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class TipoTramiteRepository : ITipoTramiteRepository
    {
        #region Miembros de ITipoTramiteRepository

    

        #region Miembros de ITipoTramiteRepository


        public IEnumerable<TipoTramite> GetTiposTramites()
        {
            List<TipoTramite> ListaTiposSocietarios = new List<TipoTramite>();
            using (var context = new Entities())
            {


               // const string sql = " select * from tabgen t where t.tabtipotab = 001 and t.tabclave != '*'";

                // Definimos la consulta SQL que ejecutará el procedimiento almacenado
                var sql = "BEGIN PK_API_LEGACY.ListTipoTramite(:p_cursor); END;";

                // Creamos un parámetro para el cursor de salida
                var outputCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                // Ejecutamos la consulta SQL y obtenemos los resultados en una lista
                var ListaTipoTramiteDB = context.Database.SqlQuery<TABGEN_PROD>(sql, outputCursor).ToList();

                //var ListaTipoTramiteDB = context.Database.SqlQuery<TABGEN_PROD>(sql).ToList();



                foreach (var tipotramitedb in ListaTipoTramiteDB)
                {

                    var tiposocietario = MapToTipoTramite(tipotramitedb);
                    ListaTiposSocietarios.Add(tiposocietario);
                }
                return ListaTiposSocietarios;
            }
        }

        public TipoTramite MapToTipoTramite(TABGEN_PROD tabgenProd)
        {

            TipoTramite tipoTramite = new TipoTramite()
            {

                Codigo = tabgenProd.TABCLAVE,
                Nombre = tabgenProd.TABCONTEN1

            };

            return tipoTramite;
        }
        #endregion

        #region Miembros de ITipoTramiteRepository


        public ResponseDTO<TipoTramite> GetTramitesbyCodigoTramite(string codigo)
        {
            ResponseDTO<TipoTramite> rsp = new ResponseDTO<TipoTramite>();
            rsp.IsSuccess = false;

            try
            {
                using (var context = new Entities())
                {
                    const string sql = " select * from tabgen t where t.tabtipotab = 001 and t.tabclave != '*' And t.tabclave =:codigo";

                    // Crear el parámetro usando Oracle.DataAccess.Client.OracleParameter
                    OracleParameter codigoParam = new OracleParameter(":codigo", OracleDbType.Varchar2) { Value = codigo };

                    // Ejecutar la consulta SQL  
                    var tipotramiteDB = context.Database.SqlQuery<TABGEN_PROD>(sql, codigoParam).FirstOrDefault();

                    //verificar si se encontro algun registro
                    if (tipotramiteDB != null)
                    {

                        // Mapear el resultado a TipoTramite
                        var tipoSocietario = MapToTipoTramite(tipotramiteDB);

                        rsp.Data = tipoSocietario;
                        rsp.Message = "OK";
                        rsp.IsSuccess = true;
                        return rsp;

                    }
                    else
                    {

                        return null;
                    }
                };
            }
            catch (Exception ex)
            {

                rsp.Message = ex.Message;
            }
           


            return rsp;

        }
        #endregion
        #endregion
    }
}
