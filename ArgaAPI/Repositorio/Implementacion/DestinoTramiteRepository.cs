using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArgaAPI.Data.ARGA;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.Data.GORDONARGA;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class DestinoTramiteRepository : IDestinoTramiteRepository
    {

      

        #region Miembros de IDestinoTramiteRepository

        public ResponseDTO<List<DestinoTramiteDTO>> GetUltimoDestinoTramite(DestinoTramite destinoTramite)
        {
            ResponseDTO<List<DestinoTramiteDTO>> rsp = new ResponseDTO<List<DestinoTramiteDTO>>();
            rsp.IsSuccess = false;

            List<DestinoTramiteDTO> listaDestinos = new List<DestinoTramiteDTO>();

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

                    command.Parameters.Add(new OracleParameter("p_nrocorr", OracleDbType.Varchar2)).Value =
                    destinoTramite.Correlativo != 0 ? (object)destinoTramite.Correlativo : DBNull.Value;
           

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
                            DestinoTramiteDTO destinoTramiteDB = new DestinoTramiteDTO
                            {
                                Correlativo = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                                Numerotramite = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                                CodigoTramite = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                                CodigoDestino = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                                UsuarioDestino = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                                NroSubdestino = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,
                                DestinoAnterior = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                                FechaComienzoTramite = reader["DTRFECHACT"] != DBNull.Value ?
                                                                               Convert.ToDateTime(reader["DTRFECHACT"]).ToString("dd/MM/yyyy") : string.Empty,
                                FechaSalidaDestino = reader["DTRFECHAST"] != DBNull.Value
                                                                          ? Convert.ToDateTime(reader["DTRFECHAST"]).ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                FechaIngresoDestino = reader["DTRFECHART"] != DBNull.Value ?
                                                                          Convert.ToDateTime(reader["DTRFECHART"]).ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,

                                //props innner join
                                RazonSocial = reader["EXPRAZONSO"] != DBNull.Value ? reader["EXPRAZONSO"].ToString() : string.Empty,
                                CodigoSocietario = reader["EXPTIPOSOC"] != DBNull.Value ? reader["EXPTIPOSOC"].ToString() : string.Empty,
                                TipoSocietario = reader["TIPO SOCIETARIO"] != DBNull.Value ? reader["TIPO SOCIETARIO"].ToString() : string.Empty,
                                DescripcionTramite = reader["TABCONTEN1"] != DBNull.Value ? reader["TABCONTEN1"].ToString() : string.Empty,
                                UrgenteNormal = reader["CDTTIPOTRAM"] != DBNull.Value ? reader["CDTTIPOTRAM"].ToString() : string.Empty,
                                RegistralInfoContable = reader["CDTREGISTRAL"] != DBNull.Value ? reader["CDTREGISTRAL"].ToString() : string.Empty,
                                AreaDestinoActual = reader["DEST_ACTUAL_AREA"] != DBNull.Value ? reader["DEST_ACTUAL_AREA"].ToString() : string.Empty,
                                DepartamentoDestinoActual = reader["DEST_ACTUAL_DEPTO"] != DBNull.Value ? reader["DEST_ACTUAL_DEPTO"].ToString() : string.Empty,
                                DestinoAnteriordArea = reader["DEST_ANTERIOR_AREA"] != DBNull.Value ? reader["DEST_ANTERIOR_AREA"].ToString() : string.Empty,
                                DestinoAnteriordDpto = reader["DEST_ANTERIOR_DEPTO"] != DBNull.Value ? reader["DEST_ANTERIOR_DEPTO"].ToString() : string.Empty,
                                NombreUsuarioRecepciona = reader["USUARIO RECEPTOR"] != DBNull.Value ? reader["USUARIO RECEPTOR"].ToString() : string.Empty,
                                UserRecepciona = reader["CODIGO RECEPTOR"] != DBNull.Value ? reader["CODIGO RECEPTOR"].ToString() : string.Empty,
                                NombreUsuarioAsigando = reader["USUARIO ASIGNADO"] != DBNull.Value ? reader["USUARIO ASIGNADO"].ToString() : string.Empty,
                                UserUsuarioAsignado = reader["CODIGO ASIGNADO"] != DBNull.Value ? reader["CODIGO ASIGNADO"].ToString() : string.Empty,
                            };


                            listaDestinos.Add(destinoTramiteDB);
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


        public ResponseDTO<List<DestinoTramiteDTO>> GetDestinosTramite(DestinoTramite destinoTramite)
        {
            ResponseDTO<List<DestinoTramiteDTO>> rsp = new ResponseDTO<List<DestinoTramiteDTO>>();
            rsp.IsSuccess = false;

            List<DestinoTramiteDTO> listaDestinos = new List<DestinoTramiteDTO>();

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
                    command.Parameters.Add(new OracleParameter("p_nrocorr", OracleDbType.Varchar2)).Value =
                    destinoTramite.Correlativo != 0 ? (object)destinoTramite.Correlativo : DBNull.Value;

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
                            DestinoTramiteDTO destinoTramiteDB = new DestinoTramiteDTO
                            {
                                Correlativo = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                                Numerotramite = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                                CodigoTramite = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                                CodigoDestino = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                                UsuarioDestino = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                                NroSubdestino = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,
                                DestinoAnterior = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                                FechaComienzoTramite = reader["DTRFECHACT"] != DBNull.Value ?
                                                                               Convert.ToDateTime(reader["DTRFECHACT"]).ToString("dd/MM/yyyy") : string.Empty,
                                FechaSalidaDestino = reader["DTRFECHAST"] != DBNull.Value
                                                                          ? Convert.ToDateTime(reader["DTRFECHAST"]).ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,
                                FechaIngresoDestino = reader["DTRFECHART"] != DBNull.Value ?
                                                                          Convert.ToDateTime(reader["DTRFECHART"]).ToString("dd/MM/yyyy HH:mm:ss") : string.Empty,

                                //props innner join
                                RazonSocial = reader["EXPRAZONSO"] != DBNull.Value ? reader["EXPRAZONSO"].ToString() : string.Empty,
                                CodigoSocietario = reader["EXPTIPOSOC"] != DBNull.Value ? reader["EXPTIPOSOC"].ToString() : string.Empty,
                                TipoSocietario = reader["TIPO SOCIETARIO"] != DBNull.Value ? reader["TIPO SOCIETARIO"].ToString() : string.Empty,
                                DescripcionTramite = reader["TABCONTEN1"] != DBNull.Value ? reader["TABCONTEN1"].ToString() : string.Empty,
                                UrgenteNormal = reader["CDTTIPOTRAM"] != DBNull.Value ? reader["CDTTIPOTRAM"].ToString() : string.Empty,
                                RegistralInfoContable = reader["CDTREGISTRAL"] != DBNull.Value ? reader["CDTREGISTRAL"].ToString() : string.Empty,
                                AreaDestinoActual = reader["DEST_ACTUAL_AREA"] != DBNull.Value ? reader["DEST_ACTUAL_AREA"].ToString() : string.Empty,
                                DepartamentoDestinoActual = reader["DEST_ACTUAL_DEPTO"] != DBNull.Value ? reader["DEST_ACTUAL_DEPTO"].ToString() : string.Empty,
                                DestinoAnteriordArea = reader["DEST_ANTERIOR_AREA"] != DBNull.Value ? reader["DEST_ANTERIOR_AREA"].ToString() : string.Empty,
                                DestinoAnteriordDpto = reader["DEST_ANTERIOR_DEPTO"] != DBNull.Value ? reader["DEST_ANTERIOR_DEPTO"].ToString() : string.Empty,
                                NombreUsuarioRecepciona = reader["USUARIO RECEPTOR"] != DBNull.Value ? reader["USUARIO RECEPTOR"].ToString() : string.Empty,
                                UserRecepciona = reader["CODIGO RECEPTOR"] != DBNull.Value ? reader["CODIGO RECEPTOR"].ToString() : string.Empty,
                                NombreUsuarioAsigando = reader["USUARIO ASIGNADO"] != DBNull.Value ? reader["USUARIO ASIGNADO"].ToString() : string.Empty,
                                UserUsuarioAsignado = reader["CODIGO ASIGNADO"] != DBNull.Value ? reader["CODIGO ASIGNADO"].ToString() : string.Empty,
                            };


                            listaDestinos.Add(destinoTramiteDB);

                            
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


  

        #endregion

        #region Miembros de IDestinoTramiteRepository


       public ResponseDTO<List<DestinoTramiteDTO>> GetTramiteSinRecibirXDestinoDpto(string destino)
{
            ResponseDTO<List<DestinoTramiteDTO>> rsp = new ResponseDTO<List<DestinoTramiteDTO>>();
            rsp.IsSuccess = false;
            List<DestinoTramiteDTO> listaDestinos = new List<DestinoTramiteDTO>();

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
                    // Crear manualmente un objeto DestinoTramiteDTO a partir del reader
                    DestinoTramiteDTO destinoTramiteDB = new DestinoTramiteDTO
                    {
                        Correlativo = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                        Numerotramite = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                        CodigoTramite = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                        CodigoDestino = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                        UsuarioDestino = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                        NroSubdestino = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,
                        DestinoAnterior = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                        FechaComienzoTramite = reader["DTRFECHACT"] != DBNull.Value 
                            ? Convert.ToDateTime(reader["DTRFECHACT"]).ToString("dd/MM/yyyy") 
                            : string.Empty,
                        FechaSalidaDestino = reader["DTRFECHAST"] != DBNull.Value 
                            ? Convert.ToDateTime(reader["DTRFECHAST"]).ToString("dd/MM/yyyy HH:mm:ss") 
                            : string.Empty,
                        FechaIngresoDestino = reader["DTRFECHART"] != DBNull.Value 
                            ? Convert.ToDateTime(reader["DTRFECHART"]).ToString("dd/MM/yyyy HH:mm:ss") 
                            : string.Empty,

                        // props innner join
                        RazonSocial = reader["EXPRAZONSO"] != DBNull.Value ? reader["EXPRAZONSO"].ToString() : string.Empty,
                        CodigoSocietario = reader["EXPTIPOSOC"] != DBNull.Value ? reader["EXPTIPOSOC"].ToString() : string.Empty,
                        TipoSocietario = reader["TIPO SOCIETARIO"] != DBNull.Value ? reader["TIPO SOCIETARIO"].ToString() : string.Empty,
                        DescripcionTramite = reader["TABCONTEN1"] != DBNull.Value ? reader["TABCONTEN1"].ToString() : string.Empty,
                        UrgenteNormal = reader["CDTTIPOTRAM"] != DBNull.Value ? reader["CDTTIPOTRAM"].ToString() : string.Empty,
                        RegistralInfoContable = reader["CDTREGISTRAL"] != DBNull.Value ? reader["CDTREGISTRAL"].ToString() : string.Empty,
                        AreaDestinoActual = reader["DEST_ACTUAL_AREA"] != DBNull.Value ? reader["DEST_ACTUAL_AREA"].ToString() : string.Empty,
                        DepartamentoDestinoActual = reader["DEST_ACTUAL_DEPTO"] != DBNull.Value ? reader["DEST_ACTUAL_DEPTO"].ToString() : string.Empty,
                        DestinoAnteriordArea = reader["DEST_ANTERIOR_AREA"] != DBNull.Value ? reader["DEST_ANTERIOR_AREA"].ToString() : string.Empty,
                        DestinoAnteriordDpto = reader["DEST_ANTERIOR_DEPTO"] != DBNull.Value ? reader["DEST_ANTERIOR_DEPTO"].ToString() : string.Empty,
                        NombreUsuarioRecepciona = reader["USUARIO RECEPTOR"] != DBNull.Value ? reader["USUARIO RECEPTOR"].ToString() : string.Empty,
                        UserRecepciona = reader["CODIGO RECEPTOR"] != DBNull.Value ? reader["CODIGO RECEPTOR"].ToString() : string.Empty,
                        NombreUsuarioAsigando = reader["USUARIO ASIGNADO"] != DBNull.Value ? reader["USUARIO ASIGNADO"].ToString() : string.Empty,
                        UserUsuarioAsignado = reader["CODIGO ASIGNADO"] != DBNull.Value ? reader["CODIGO ASIGNADO"].ToString() : string.Empty,
                         };

                        listaDestinos.Add(destinoTramiteDB);
                        }
                    }

                        rsp.Data = listaDestinos;
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

        #region Miembros de IDestinoTramiteRepository


        public ResponseDTO<bool> RecibirAsignarSubDestinTramite(DestinoTramite destinoTramite)
        {
          var rst = new ResponseDTO<bool>
        {
            IsSuccess = false,
            Data = false
        };

        try
        {
            using (var context = new GORDON())
            {
                // Ejecutar el procedimiento almacenado mediante EF
                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("p_codigodestino", OracleDbType.Varchar2) 
                    { Value = destinoTramite.CodigoDestino ?? (object)DBNull.Value },
                    
                    new OracleParameter("p_nrotram", OracleDbType.Int32) 
                    { Value = destinoTramite.Numerotramite ?? (object)DBNull.Value },
                    
                    new OracleParameter("p_correlativo", OracleDbType.Int32) 
                    { Value = destinoTramite.Correlativo },
                    
                    new OracleParameter("p_codigoUsuarioReceptor", OracleDbType.Int32) 
                        {
                            Value = !string.IsNullOrEmpty(destinoTramite.UsuarioDestino) 
                                ? (object)Convert.ToInt32(destinoTramite.UsuarioDestino) 
                                : DBNull.Value
                        },
                    new OracleParameter("p_nrosubestinoUser", OracleDbType.Int32) 
                    { Value = destinoTramite.NroSubdestino ?? (object)DBNull.Value }
                };

                // Ejecutar el procedimiento almacenado
                context.Database.ExecuteSqlCommand(
                    "BEGIN PK_API_LEGACY.RecibirAsignarSubDestinTramite(:p_codigodestino, :p_nrotram, :p_correlativo, :p_codigoUsuarioReceptor, :p_nrosubestinoUser); END;",
                    parameters.ToArray()
                );

                // Configurar respuesta
                rst.IsSuccess = true;
                rst.Data = true;
                rst.Message = "Actualización realizada correctamente.";
            }
        }
        catch (Exception ex)
        {
            rst.Message =  ex.Message +"--"+ ex.InnerException.Message;
        }

        return rst;
        }


        #endregion

        #region Miembros de IDestinoTramiteRepository


        public ResponseDTO<bool> EnviarTramiteAOtroDestino(DestinoTramiteDTO destinoTramite)
        {
        var rst = new ResponseDTO<bool>
    {
        IsSuccess = false,
        Data = false
    };

    try
    {
        // Validar que el objeto no sea nulo
        if (destinoTramite == null)
        {
            throw new ArgumentNullException("destinoTramite", "El objeto destinoTramite es nulo.");
        }

        // Validar las propiedades necesarias
        if (destinoTramite.CodigoTramite == null || destinoTramite.DestinoHacia == null)
        {
            throw new ArgumentNullException("destinoTramite", "El objeto destinoTramite es nulo.");
        }

            using (var context = new GORDON())
            {
                if (context == null)
                {
                    throw new InvalidOperationException("La conexión a la base de datos no está inicializada.");
                }

              

                var parameters = new List<OracleParameter>
                {
                    new OracleParameter("p_nrocorrelativo", OracleDbType.Int32) 
                    { Value = destinoTramite.Correlativo },

                    new OracleParameter("p_codigotramite", OracleDbType.Varchar2) 
                    { Value = destinoTramite.CodigoTramite ?? (object)DBNull.Value },
                
                    new OracleParameter("p_nrotramite", OracleDbType.Int32) 
                    { Value = destinoTramite.Numerotramite ?? (object)DBNull.Value },
                     
                    new OracleParameter("p_fechainicio", OracleDbType.Varchar2) 
                    { Value = destinoTramite.FechaComienzoTramite },

                    new OracleParameter("p_destinoactual", OracleDbType.Varchar2) 
                    { Value = destinoTramite.CodigoDestino ?? (object)DBNull.Value },

                    new OracleParameter("p_destinohacia", OracleDbType.Varchar2) 
                    { Value = destinoTramite.DestinoHacia ?? (object)DBNull.Value},
                };

                context.Database.Connection.Open();

                using (var transaction = context.Database.Connection.BeginTransaction())
                {

                    try
                    {
                        OracleCommand oc = new OracleCommand("pk_api_legacy.enviartramiteaotrodestino", (OracleConnection)context.Database.Connection);

                        oc.Parameters.AddRange(parameters.ToArray());
                        oc.CommandType = CommandType.StoredProcedure;
                        oc.Transaction = (OracleTransaction)transaction;
                        
                        int rlt = oc.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally { 
                        context.Database.Connection.Close(); 
                    }
                    
                }
                
/*
                        context.Database.ExecuteSqlCommand(
                            "begin   pk_api_legacy.enviartramiteaotrodestino(p_nrocorrelativo => :p_nrocorrelativo,p_codigotramite => :p_codigotramite,p_nrotramite => :p_nrotramite,p_fechainicio => :p_fechainicio,p_destinoactual => :p_destinoactual,p_destinohacia => :p_destinohacia);end;",
                            parameters.ToArray()
                        );
                
                        transaction.Commit();*/
                        rst.IsSuccess = true;
                        rst.Data = true;
                        rst.Message = "Actualización realizada correctamente.";
                
              //  }
               
            }
        }
        catch (Exception ex)
        {
            rst.Message = ex.Message;
            if (ex.InnerException != null)
            {
                rst.Message += ex.InnerException.Message;
            }
        }

        return rst;
        }

        #endregion

        #region Miembros de IDestinoTramiteRepository


        public ResponseDTO<List<DestinoTramiteDTO>> GetTramitesRecibidosXDestinoDepto(DestinoTramiteDTO destinoTramite)
        {
            ResponseDTO<List<DestinoTramiteDTO>> rsp = new ResponseDTO<List<DestinoTramiteDTO>>();
            rsp.IsSuccess = false;

            List<DestinoTramiteDTO> listaDestinos = new List<DestinoTramiteDTO>();

            try
            {
                using (var context = new DEVIGJ())
                {

                    // Crear el comando usando la conexión del contexto
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.TramitesRecibidosXDestinDto", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros del procedimiento almacenado

                    command.Parameters.Add(new OracleParameter("p_coddest", OracleDbType.Varchar2)).Value =
                    !string.IsNullOrEmpty(destinoTramite.CodigoDestino) ? destinoTramite.CodigoDestino : (object)DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_dtrnrosubd", OracleDbType.Int32)).Value =
                        destinoTramite.NroSubdestino != 0 ? (object)destinoTramite.NroSubdestino : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_nrocorr", OracleDbType.Varchar2)).Value =
                    destinoTramite.Correlativo != 0 ? (object)destinoTramite.Correlativo : DBNull.Value;

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
                            DestinoTramiteDTO destinoTramiteDB = new DestinoTramiteDTO
                            {
                                Correlativo = reader["DTRNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROCORR"]) : 0,
                                Numerotramite = reader["DTRNROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["DTRNROTRAM"]) : 0,
                                CodigoTramite = reader["DTRCODTRAM"] != DBNull.Value ? reader["DTRCODTRAM"].ToString() : string.Empty,
                                CodigoDestino = reader["DTRCODDEST"] != DBNull.Value ? reader["DTRCODDEST"].ToString() : string.Empty,
                                UsuarioDestino = reader["DTRUSUDEST"] != DBNull.Value ? reader["DTRUSUDEST"].ToString() : string.Empty,
                                NroSubdestino = reader["DTRNROSUBD"] != DBNull.Value ? Convert.ToInt16(reader["DTRNROSUBD"]) : (short)0,
                                DestinoAnterior = reader["DTRDESTANT"] != DBNull.Value ? reader["DTRDESTANT"].ToString() : string.Empty,
                                FechaComienzoTramite = reader["DTRFECHACT"] != DBNull.Value
                             ? Convert.ToDateTime(reader["DTRFECHACT"]).ToString("dd/MM/yyyy")
                             : string.Empty,
                                FechaSalidaDestino = reader["DTRFECHAST"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["DTRFECHAST"]).ToString("dd/MM/yyyy HH:mm:ss")
                                    : string.Empty,
                                FechaIngresoDestino = reader["DTRFECHART"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["DTRFECHART"]).ToString("dd/MM/yyyy HH:mm:ss")
                                    : string.Empty,

                                //props innner join
                                RazonSocial = reader["EXPRAZONSO"] != DBNull.Value ? reader["EXPRAZONSO"].ToString() : string.Empty,
                                CodigoSocietario = reader["EXPTIPOSOC"] != DBNull.Value ? reader["EXPTIPOSOC"].ToString() : string.Empty,
                                TipoSocietario = reader["TIPO SOCIETARIO"] != DBNull.Value ? reader["TIPO SOCIETARIO"].ToString() : string.Empty,
                                DescripcionTramite = reader["TABCONTEN1"] != DBNull.Value ? reader["TABCONTEN1"].ToString() : string.Empty,
                                UrgenteNormal = reader["CDTTIPOTRAM"] != DBNull.Value ? reader["CDTTIPOTRAM"].ToString() : string.Empty,
                                RegistralInfoContable = reader["CDTREGISTRAL"] != DBNull.Value ? reader["CDTREGISTRAL"].ToString() : string.Empty,
                                AreaDestinoActual = reader["DEST_ACTUAL_AREA"] != DBNull.Value ? reader["DEST_ACTUAL_AREA"].ToString() : string.Empty,
                                DepartamentoDestinoActual = reader["DEST_ACTUAL_DEPTO"] != DBNull.Value ? reader["DEST_ACTUAL_DEPTO"].ToString() : string.Empty,
                                DestinoAnteriordArea = reader["DEST_ANTERIOR_AREA"] != DBNull.Value ? reader["DEST_ANTERIOR_AREA"].ToString() : string.Empty,
                                DestinoAnteriordDpto = reader["DEST_ANTERIOR_DEPTO"] != DBNull.Value ? reader["DEST_ANTERIOR_DEPTO"].ToString() : string.Empty,
                                NombreUsuarioRecepciona = reader["USUARIO RECEPTOR"] != DBNull.Value ? reader["USUARIO RECEPTOR"].ToString() : string.Empty,
                                UserRecepciona = reader["CODIGO RECEPTOR"] != DBNull.Value ? reader["CODIGO RECEPTOR"].ToString() : string.Empty,
                                NombreUsuarioAsigando = reader["USUARIO ASIGNADO"] != DBNull.Value ? reader["USUARIO ASIGNADO"].ToString() : string.Empty,
                                UserUsuarioAsignado = reader["CODIGO ASIGNADO"] != DBNull.Value ? reader["CODIGO ASIGNADO"].ToString() : string.Empty,


                            };
                            listaDestinos.Add(destinoTramiteDB);
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