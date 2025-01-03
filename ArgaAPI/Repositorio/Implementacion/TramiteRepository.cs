﻿using System;
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
                using (var context = new DEVIGJ())
                {
                    // Crear el comando SQL
                    string query = "SELECT t.* " +
                                   "FROM TRAMIT t " +
                                   "WHERE t.tranrocorr = :p_Correlativo " +
                                   "AND (:p_NroTramite IS NULL OR t.TRANROTRAM = :p_NroTramite) " +
                                   "AND t.TRAGRPTRAM IS NULL " +
                                   "ORDER BY t.TRAFECHACT DESC";

                    OracleCommand command = new OracleCommand(query, (OracleConnection)context.Database.Connection);
                    command.CommandType = CommandType.Text;

                    // Definir los parámetros de la consulta
                    command.Parameters.Add(new OracleParameter("p_Correlativo", OracleDbType.Varchar2)).Value =
                        tramite.Correlativo != 0 ? (object)tramite.Correlativo.ToString() : DBNull.Value;

                    command.Parameters.Add(new OracleParameter("p_NroTramite", OracleDbType.Varchar2)).Value =
                        tramite.NroTramite.HasValue ? (object)tramite.NroTramite.Value.ToString() : DBNull.Value;

                    // Abrir la conexión si no está abierta
                    if (context.Database.Connection.State != ConnectionState.Open)
                    {
                        context.Database.Connection.Open();
                    }

                    // Ejecutar la consulta SQL y leer los resultados
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tramite tramiteDb = new Tramite()
                            {
                                NroTramite = reader["TRANROTRAM"] != DBNull.Value ? Convert.ToInt32(reader["TRANROTRAM"]) : 0,
                                CodigoTramite = reader["TRACODTRAM"] != DBNull.Value ? reader["TRACODTRAM"].ToString() : string.Empty,
                                Correlativo = reader["TRANROCORR"] != DBNull.Value ? Convert.ToInt32(reader["TRANROCORR"]) : 0,
                                FechaRegistracion = reader["TRAFECHACT"] != DBNull.Value ? Convert.ToDateTime(reader["TRAFECHACT"]) : (DateTime?)null,
                            };
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


       

        #endregion

        public Tramite MapToTramite(TRAMIT tramitProd)
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

        public Tramite MapTramitToTramite(TRAMIT tramit)
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