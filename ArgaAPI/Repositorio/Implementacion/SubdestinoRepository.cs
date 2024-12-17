using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class SubdestinoRepository : ISubdestinoRepository
    {
        #region Miembros de ISubdestinoRepository

        public DTOs.ResponseDTO<List<Subdestino>> GetAgentesXSubdestino(string subdestino)
        {
            ResponseDTO<List<Subdestino>> rsp = new ResponseDTO<List<Subdestino>>();
            rsp.IsSuccess = false;

            List<Subdestino> listaSubdestinos = new List<Subdestino>();

            try
            {
                using (var context = new DEVIGJ())
                {

                    // Crear el comando usando la conexión del contexto
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.AgentesPorSubdestino", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros del procedimiento almacenado

                    command.Parameters.Add(new OracleParameter("p_subdestino", OracleDbType.Varchar2)).Value =
                    !string.IsNullOrEmpty(subdestino) ? subdestino : (object)DBNull.Value;

                 

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
                            // Crear manualmente un objeto DestinoTramiteDTO a partir del reader
                            Subdestino subdestinoDB = new Subdestino
                            {
                                CodigoAgenteSubdestino = reader["SUBUSUARIO"] != DBNull.Value ? Convert.ToInt32(reader["SUBUSUARIO"]) : 0,
                                NroSubdestino = reader["SUBNROSUBD"] != DBNull.Value ? Convert.ToInt32(reader["SUBNROSUBD"]) : 0,
                                NombreAgente = reader["SUBNOMBRE"] != DBNull.Value ? reader["SUBNOMBRE"].ToString() : string.Empty,
                                NombreSubdestino = reader["SUBDESTINO"] != DBNull.Value ? reader["SUBDESTINO"].ToString() : string.Empty,
                               


                            };
                            listaSubdestinos.Add(subdestinoDB);
                        }
                    }

                    rsp.Data = listaSubdestinos;
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
    }
}