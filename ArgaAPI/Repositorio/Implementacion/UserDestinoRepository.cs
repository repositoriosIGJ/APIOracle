using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class UserDestinoRepository : IUserDestinoRepository
    {
        #region Miembros de IUserRepository

        public ResponseDTO<List<UserDestino>> GetDestinosUser(string username)
        {
            ResponseDTO<List<UserDestino>> rsp = new ResponseDTO<List<UserDestino>>();
            rsp.IsSuccess = false;
            List<UserDestino> listaDestinosUsario = new List<UserDestino>();

            try
            {
                using (var context = new DEVIGJ())
                {
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.GetDestinosUser", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    command.Parameters.Add(new OracleParameter("p_username", OracleDbType.Varchar2)).Value = username;

                    // Parámetro de salida (cursor)
                    OracleParameter outputCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputCursor);

                    // Abrir conexión
                    if (context.Database.Connection.State != ConnectionState.Open)
                    {
                        context.Database.Connection.Open();
                    }

                    // Ejecutar procedimiento almacenado
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crear manualmente un objeto DestinoTramiteDTO a partir del reader
                            UserDestino usuariodestinoDB = new UserDestino
                            {

                                CodigoUsuario = reader["Codigo Usuario"] != DBNull.Value ? reader["Codigo Usuario"].ToString() : string.Empty,
                                User = reader["User"] != DBNull.Value ? reader["User"].ToString() : string.Empty,
                                UserBPM = reader["UserBPM"] != DBNull.Value ? reader["UserBPM"].ToString() : string.Empty,
                                NombreCompleto = reader["Nombre Completo"] != DBNull.Value ? reader["Nombre Completo"].ToString() : string.Empty,
                                Destino = reader["Destino"] != DBNull.Value ? reader["Destino"].ToString() : string.Empty,
                                Area = reader["Area"] != DBNull.Value ? reader["Area"].ToString() : string.Empty,
                                Departamtento = reader["Departamento"] != DBNull.Value ? reader["Departamento"].ToString() : string.Empty

                            
                            };

                            listaDestinosUsario.Add(usuariodestinoDB);
                        }
                    }

                    rsp.Data = listaDestinosUsario;
                    rsp.Message = "Ok";
                    rsp.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                rsp.Message = ex.Message + "-" + ex.InnerException;
            }

            return rsp;
        }

        #endregion
    }
}