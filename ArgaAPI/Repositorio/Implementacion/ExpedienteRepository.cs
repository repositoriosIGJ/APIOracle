using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ArgaAPI.Data;
using ArgaAPI.Data.ARGA;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class ExpedienteRepository : IExpedienteRepository
    {

        private readonly ITipoSocietarioReposity _tiposocietarioRepository;

        public ExpedienteRepository(ITipoSocietarioReposity tiposocietarioRepository)
        {
            _tiposocietarioRepository = tiposocietarioRepository;
        }
        #region Miembros de IExpedienteRepository

       

       


        #endregion

        public Expediente MapEXPTEToExpediente(EXPTES exptes) {
          TipoSocietario tiposocietario = new TipoSocietario();

          if (exptes.EXPTIPOSOC < 100)
          {

              string numeroConCeroIzquierda = exptes.EXPTIPOSOC.ToString("D3");
              tiposocietario = _tiposocietarioRepository.GetTipoSocietarioPorCodigo(numeroConCeroIzquierda);
          }
          else
          {
              tiposocietario = _tiposocietarioRepository.GetTipoSocietarioPorCodigo(exptes.EXPTIPOSOC.ToString());

          }
            Expediente expediente = new Expediente()
            {
                Correlativo = exptes.EXPNROCORR,
                RazonSocial = exptes.EXPRAZONSO,
                Referencial = (int)exptes.EXPEXPREFE,
                TipoSocietario = tiposocietario.Acronimo.Trim(),
                CodigoTipoSocietario = exptes.EXPTIPOSOC,
                Cuil = exptes.EXPNROCUIT
            };

            return expediente;
        }

        #region Miembros de IExpedienteRepository


        public ResponseDTO<List<Expediente>> GetExpedientes(Expediente expediente)
        {
            ResponseDTO<List<Expediente>> rsp = new ResponseDTO<List<Expediente>>();
            rsp.IsSuccess = false;

            List<Expediente> listaExpediente = new List<Expediente>();


            try
            {
                using (var context = new DEVIGJ())
                {

                    // Crear el comando usando la conexión del contexto
                    OracleCommand command = new OracleCommand("PK_API_LEGACY.buscarxRazonSocial", (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros del procedimiento almacenado
                    command.Parameters.Add(new OracleParameter("p_RazonSocial", OracleDbType.Varchar2)).Value =
                    !string.IsNullOrEmpty(expediente.RazonSocial) ? (object)expediente.RazonSocial.ToUpper() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_cuit", OracleDbType.Varchar2)).Value =
                        expediente.Cuil.HasValue ? (object)expediente.Cuil.ToString() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_refe", OracleDbType.Varchar2)).Value =
                        expediente.Referencial != 0 ? (object)expediente.Referencial.ToString() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_tiposoc", OracleDbType.Varchar2)).Value =
                        expediente.CodigoTipoSocietario != 0 ? (object)expediente.CodigoTipoSocietario.ToString() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_nrocorr", OracleDbType.Varchar2)).Value =
                        expediente.Correlativo != 0 ? (object)expediente.Correlativo.ToString() : DBNull.Value;

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
                            // Crear manualmente un objeto EXPTES_PROD a partir del reader
                            EXPTES exptes = new EXPTES
                            {
                                //CONDICIONAL PARA QUE AGREGARLE VALOR A LOS PARAMETROS NULOS 0 A LOS ENTEROS Y "" AL STRING 
                                EXPNROCORR = reader["EXPNROCORR"] != DBNull.Value ? Convert.ToInt32(reader["EXPNROCORR"]) : 0,
                                EXPRAZONSO = reader["EXPRAZONSO"] != DBNull.Value ? reader["EXPRAZONSO"].ToString() : string.Empty,
                                EXPTIPOSOC = reader["EXPTIPOSOC"] != DBNull.Value ? Convert.ToInt16(reader["EXPTIPOSOC"]) : (short)0,
                                EXPNROCUIT = reader["EXPNROCUIT"] != DBNull.Value ? Convert.ToInt64(reader["EXPNROCUIT"]) : 0,
                                EXPEXPREFE = reader["EXPEXPREFE"] != DBNull.Value ? Convert.ToInt32(reader["EXPEXPREFE"]) : 0,
                            };

                            // Mapear el objeto EXPTES_PROD a Expediente
                            Expediente expedienteDb = MapEXPTEToExpediente(exptes);

                            listaExpediente.Add(expedienteDb);
                        }
                    }

                    rsp.Data = listaExpediente;
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