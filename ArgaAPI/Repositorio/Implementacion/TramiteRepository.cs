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
    public class TramiteRepository : ITramiteRepository
    {
        private readonly ITipoTramiteRepository _tipoTramiteRepository;

        public TramiteRepository(ITipoTramiteRepository tipoTramiteRepository)
        {
           _tipoTramiteRepository = tipoTramiteRepository;
        }
        #region Miembros de ITramiteRepository

        public ResponseDTO<List<Tramite>> GetTramites(Tramite tramite)
        {
            ResponseDTO<List<Tramite>> rsp = new ResponseDTO<List<Tramite>>();
            rsp.IsSuccess = false;

            List<Tramite> listaTramites = new List<Tramite>();

            try
            {
                using (var context = new Entities())
                {
                    // Crear el comando usando la conexión del contexto
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.buscarTramites", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros del procedimiento almacenado

                    // Convertir a cadena y manejar DBNull.Value para valores nulos
                    command.Parameters.Add(new OracleParameter("p_NroTramite", OracleDbType.Varchar2)).Value =
                        tramite.NroTramite.HasValue ? (object)tramite.NroTramite.Value.ToString() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_CodigoTramite", OracleDbType.Varchar2)).Value =
                        !string.IsNullOrEmpty(tramite.CodigoTramite) ? (object)tramite.CodigoTramite : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_Correlativo", OracleDbType.Varchar2)).Value =
                        tramite.Correlativo != 0 ? (object)tramite.Correlativo.ToString() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_FechaRegistracion", OracleDbType.Varchar2)).Value =
                        tramite.FechaRegistracion.HasValue ? (object)tramite.FechaRegistracion.Value.ToString("YYYY-MM-dd") : DBNull.Value;

                    // Parámetro de salida que obtendrá el cursor
                    OracleParameter outputCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputCursor);

                    // Abrir la conexión si no está abierta
                    if (context.Database.Connection.State != ConnectionState.Open)
                    {
                        context.Database.Connection.Open();
                    }

                    // Ejecutar el procedimiento almacenado y leer el cursor de salida
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear manualmente un objeto TRAMIT_PROD a partir del reader
                            TRAMIT_PROD tramit = new TRAMIT_PROD
                            {
                                // Manejar valores nulos
                                TRANROTRAM = reader["TRANROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["TRANROTRAM"]) : 0,
                                TRACODTRAM = reader["TRACODTRAM"] != DBNull.Value ? reader["TRACODTRAM"].ToString() : string.Empty,
                                TRANROCORR = reader["TRANROCORR"] != DBNull.Value ? Convert.ToInt32(reader["TRANROCORR"]) : 0,
                                TRAFECHACT = reader["TRAFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["TRAFECHACT"]) : (DateTime?)null,
                            };

                            // Mapear el objeto TRAMIT_PROD a Tramite
                            Tramite tramiteDb = MapTramitToTramite(tramit);

                            listaTramites.Add(tramiteDb);
                        }
                    }

                    rsp.Data = listaTramites;
                    rsp.Message = "Ok";
                    rsp.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                rsp.Message = ex.Message;
            }

            return rsp;
        }
        



        #endregion

        #region Miembros de ITramiteRepository


        public ResponseDTO<List<Tramite>> GetTramitesbyCorrelativo(int correlativo)
        {
            ResponseDTO<List<Tramite>> rsp = new ResponseDTO<List<Tramite>>();
            List<Tramite> listaTramites = new List<Tramite>();

            rsp.IsSuccess = false;

            using (var context = new Entities())
            {
                string sql = "select * from tramit_prod t where t.tranrocorr = :correlativo  order by t.tracodtram desc";

                // Crear el parámetro usando Oracle.DataAccess.Client.OracleParameter
                OracleParameter tipoSocParam = new OracleParameter(":tipo", OracleDbType.Int32) { Value = correlativo };

                // Ejecutar la consulta SQL pasando el parámetro
                var tramitesDB = context.Database.SqlQuery<TRAMIT_PROD>(sql, tipoSocParam);

                if (tramitesDB != null)
                {
                    // Iteramos sobre cada elemento de la lista devuelta por el procedimiento
                    foreach (var tramitedb in tramitesDB)
                    {
                        // Mapeamos el objeto TRAMIT_PROD al objeto TipoSocietario
                        Tramite tramite = MapToTramite(tramitedb);

                        // Añadimos el objeto tramite a la lista final
                        listaTramites.Add(tramite);

                    }
                    rsp.Data = listaTramites;
                    rsp.IsSuccess = true;
                    rsp.Message = "Ok";
                }
                else
                {
                    return null;
                }


                return rsp;
            }
        }

        #endregion

        public Tramite MapToTramite(TRAMIT_PROD tramitProd)
        {
           
            Tramite tramite = new Tramite()
            {

                NroTramite = tramitProd.TRANROTRAM,
                Correlativo = tramitProd.TRANROCORR,
                CodigoTramite = tramitProd.TRACODTRAM,
                FechaRegistracion = tramitProd.TRAFECREG,
                
            };

            return tramite;
        }

        public Tramite MapTramitToTramite(TRAMIT_PROD tramit)
        {

            var tramitedb = _tipoTramiteRepository.GetTramitesbyCodigoTramite(tramit.TRACODTRAM);
         
            Tramite tramite = new Tramite()
            {
                Correlativo = tramit.TRANROCORR,
                CodigoTramite = tramit.TRACODTRAM,
                NroTramite = tramit.TRANROTRAM,
                FechaRegistracion = tramit.TRAFECHACT,
                TipoTramite = tramitedb.Data.Nombre,
            };

            return tramite;
        }
    }
}