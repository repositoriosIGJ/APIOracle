using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArgaAPI.Data.ARGA;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class DestinoTramiteRepository : IDestinoTramiteRepository
    {

      

        #region Miembros de IDestinoTramiteRepository

        public ResponseDTO<List<DestinoTramite>> GetUltimoDestinoTramite(DestinoTramite destinoTramite)
        {
            ResponseDTO<List<DestinoTramite>> rsp = new ResponseDTO<List<DestinoTramite>>();
            rsp.IsSuccess = false;

            List<DestinoTramite> listaDestinos = new List<DestinoTramite>();

            try
            {
                using (var context = new DEVIGJ())
                {

                    // Crear el comando usando la conexión del contexto
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.buscarUltimoDestinoTramite", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros del procedimiento almacenado
                    command.Parameters.Add(new OracleParameter("p_nrotram", OracleDbType.Varchar2)).Value =
                    destinoTramite.Numerotramite != 0 ? (object)destinoTramite.Numerotramite : DBNull.Value;

                    /* command.Parameters.Add(new OracleParameter("p_nrocorr", OracleDbType.Varchar2)).Value =
                     destinoTramite.Correlativo != 0 ? (object)destinoTramite.Correlativo : DBNull.Value;
                   
                   

                     command.Parameters.Add(new OracleParameter("p_codtram", OracleDbType.Varchar2)).Value =
                     !string.IsNullOrEmpty(destinoTramite.CodigoTramite) ? (object)destinoTramite.CodigoTramite : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_coddest", OracleDbType.Varchar2)).Value =
                     !string.IsNullOrEmpty(destinoTramite.CodigoDestino) ? (object)destinoTramite.CodigoDestino : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_usudest", OracleDbType.Varchar2)).Value =
                     !string.IsNullOrEmpty(destinoTramite.UsuarioDestino) ? (object)destinoTramite.UsuarioDestino : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_nrosubdest", OracleDbType.Varchar2)).Value =
                     destinoTramite.NroSubdestino != 0 ? (object)destinoTramite.NroSubdestino : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_destant", OracleDbType.Varchar2)).Value =
                     !string.IsNullOrEmpty(destinoTramite.DestinoAnterior) ? (object)destinoTramite.DestinoAnterior : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_fechacomiezotram", OracleDbType.Varchar2)).Value =
                     destinoTramite.FechaComienzoTramite.HasValue ? (object)destinoTramite.FechaComienzoTramite.Value.ToString("YYYY-MM-dd") : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_fechacomienzodest", OracleDbType.Varchar2)).Value =
                     destinoTramite.FechaIngresoDestino.HasValue ? (object)destinoTramite.FechaIngresoDestino.Value.ToString("YYYY-MM-dd") : DBNull.Value;

                     command.Parameters.Add(new OracleParameter("p_fechasalidadest", OracleDbType.Varchar2)).Value =
                     destinoTramite.FechaSalidaDestino.HasValue ? (object)destinoTramite.FechaSalidaDestino.Value.ToString("YYYY-MM-dd") : DBNull.Value;*/

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
                            // Crear manualmente un objeto DESTRA a partir del reader
                            DESTRA destra = new DESTRA
                            {
                                //CONDICIONAL PARA QUE AGREGARLE VALOR A LOS PARAMETROS NULOS 0 A LOS ENTEROS Y "" AL STRING 
                                DTRNROCORR = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                                DTRNROTRAM = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                                DTRCODTRAM = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                                DTRCODDEST = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                                DTRUSUDEST = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                                DTRNROSUBD = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,

                                DTRDESTANT = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                                DTRFECHACT = reader["DTRFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHACT"]) : (DateTime?)null,
                                DTRFECHAST = reader["DTRFECHAST"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHAST"]) : (DateTime?)null,
                                DTRFECHART = reader["DTRFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHACT"]) : (DateTime?)null,
                            };

                            // Mapear el objeto DESTRA a DestinoTramite
                            DestinoTramite destinoTramiteeDb = MapDESTRAToDestinoTramite(destra);

                            listaDestinos.Add(destinoTramiteeDb);
                        }
                    }

                    rsp.Data = listaDestinos;
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


        private DestinoTramite MapDESTRAToDestinoTramite(DESTRA destra)
        {
            DestinoTramite destinoTramite = new DestinoTramite
            {
                Correlativo = destra.DTRNROCORR,
                Numerotramite = destra.DTRNROTRAM,
                CodigoTramite = destra.DTRCODTRAM,
                CodigoDestino = destra.DTRCODDEST,
                DestinoAnterior = destra.DTRDESTANT,
                UsuarioDestino = destra.DTRUSUDEST,
                NroSubdestino = destra.DTRNROSUBD,
                FechaComienzoTramite = destra.DTRFECHACT,
                FechaIngresoDestino = destra.DTRFECHART,
                FechaSalidaDestino = destra.DTRFECHAST

            };

            return destinoTramite;
        }

        #endregion

        #region Miembros de IDestinoTramiteRepository


        public ResponseDTO<List<DestinoTramite>> GetDestinosTramite(DestinoTramite destinoTramite)
        {
            ResponseDTO<List<DestinoTramite>> rsp = new ResponseDTO<List<DestinoTramite>>();
            rsp.IsSuccess = false;

            List<DestinoTramite> listaDestinos = new List<DestinoTramite>();

            try
            {
                using (var context = new DEVIGJ())
                {

                    // Crear el comando usando la conexión del contexto
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.buscarDestinosTramite", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros del procedimiento almacenado
                    command.Parameters.Add(new OracleParameter("p_nrotram", OracleDbType.Varchar2)).Value =
                    destinoTramite.Numerotramite != 0 ? (object)destinoTramite.Numerotramite : DBNull.Value;


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
                            // Crear manualmente un objeto DESTRA a partir del reader
                            DESTRA destra = new DESTRA
                            {
                                //CONDICIONAL PARA QUE AGREGARLE VALOR A LOS PARAMETROS NULOS 0 A LOS ENTEROS Y "" AL STRING 
                                DTRNROCORR = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                                DTRNROTRAM = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                                DTRCODTRAM = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                                DTRCODDEST = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                                DTRUSUDEST = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                                DTRNROSUBD = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,

                                DTRDESTANT = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                                DTRFECHACT = reader["DTRFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHACT"]) : (DateTime?)null,
                                DTRFECHAST = reader["DTRFECHAST"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHAST"]) : (DateTime?)null,
                                DTRFECHART = reader["DTRFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHACT"]) : (DateTime?)null,
                            };

                            // Mapear el objeto DESTRA a DestinoTramite
                            DestinoTramite destinoTramiteeDb = MapDESTRAToDestinoTramite(destra);

                            listaDestinos.Add(destinoTramiteeDb);
                        }
                    }

                    rsp.Data = listaDestinos;
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

        #region Miembros de IDestinoTramiteRepository


        public ResponseDTO<List<DestinoTramite>> GetTramiteSinRecibirXDestinoDpto(string destino)
        {
            ResponseDTO<List<DestinoTramite>> rsp = new ResponseDTO<List<DestinoTramite>>();
            rsp.IsSuccess = false;
            List<DestinoTramite> listaDestinos = new List<DestinoTramite>();

            try
            {
                using (var context = new DEVIGJ())
                {
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.TramitSinRecibir999XDestinDto", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    command.Parameters.Add(new OracleParameter("p_coddest", OracleDbType.Varchar2)).Value = destino;

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
                            DESTRA destra = new DESTRA
                            {
                                DTRNROCORR = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                                DTRNROTRAM = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                                DTRCODTRAM = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                                DTRCODDEST = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                                DTRUSUDEST = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                                DTRNROSUBD = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,

                                DTRDESTANT = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                                DTRFECHACT = reader["DTRFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHACT"]) : (DateTime?)null,
                                DTRFECHAST = reader["DTRFECHAST"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHAST"]) : (DateTime?)null,
                                DTRFECHART = reader["DTRFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["DTRFECHACT"]) : (DateTime?)null,
                            };

                            DestinoTramite destinoTramiteeDb = MapDESTRAToDestinoTramite(destra);
                            listaDestinos.Add(destinoTramiteeDb);
                        }
                    }

                    rsp.Data = listaDestinos;
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