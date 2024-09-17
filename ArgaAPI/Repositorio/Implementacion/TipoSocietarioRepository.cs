using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Repositorio.Contrato;
using ArgaAPI.Data ;
using Newtonsoft.Json;
using ArgaAPI.Models;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;
using ArgaAPI.DTOs;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.Data.ARGA;


namespace ArgaAPI.Repositorio.Implementacion
{
    public class TipoSocietarioRepository : ITipoSocietarioReposity
    {



        #region Miembros de ITipoSocietarioReposity

        public  ResponseDTO<IEnumerable<TipoSocietario>> GetTiposSocietarios()
        {
            ResponseDTO<IEnumerable<TipoSocietario>> rst = new ResponseDTO<IEnumerable<TipoSocietario>>();
            rst.IsSuccess = false;

            List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();

            // Usamos el contexto de Entity Framework para interactuar con la base de datos
            using (var context = new DEVIGJ())
            {
                try
                {
                    // Definimos la consulta SQL que ejecutará el procedimiento almacenado
                    var sql = "BEGIN PK_API_LEGACY.ListTiposSocietarios(:p_cursor); END;";

                    // Creamos un parámetro para el cursor de salida
                    var outputCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                    // Ejecutamos la consulta SQL y obtenemos los resultados en una lista
                    var ListaTipoSocietarioDB = context.Database.SqlQuery<TABGEN>(sql, outputCursor).ToList();

                    // Iteramos sobre cada elemento de la lista devuelta por el procedimiento
                    foreach (var tipoSocietariodb in ListaTipoSocietarioDB)
                    {
                        // Mapeamos el objeto TABGEN_PROD al objeto TipoSocietario
                        TipoSocietario tiposocietario = MapToTipoSocietario(tipoSocietariodb);

                        // Añadimos el objeto TipoSocietario a la lista final
                        ListaTiposSocietarios.Add(tiposocietario);
                    }

                    rst.Data = ListaTiposSocietarios;
                    rst.IsSuccess = true;
                    rst.Message = "OK";
                }
                catch (Exception ex)
                {

                    rst.Message = ex.Message;
                }
               
            }
           
            return rst;
        }


    
        public TipoSocietario MapToTipoSocietario(TABGEN tabgenProd)
        {

            TipoSocietario tiposocietario = new TipoSocietario()
            {

                Codigo = tabgenProd.TABCLAVE,
                Nombre = tabgenProd.TABCONTEN1,
                Acronimo = tabgenProd.TABCONTEN2

            };

            return tiposocietario;
        }

        public TABGEN MapToTabGen(TipoSocietario tipoSocietario)
        {

            TABGEN tabgen = new TABGEN()
            {
                TABTIPOTAB = "002",
                TABCLAVE = tipoSocietario.Codigo,
                TABCONTEN1 =tipoSocietario.Nombre,
                TABCONTEN2 = tipoSocietario.Acronimo
               

            };

            return tabgen;
        }
        
        #region Miembros de ITipoSocietarioReposity


        public  ResponseDTO<TipoSocietario> GetTipoSocietarioPorCodigo(string codigo)
        {
             ResponseDTO<TipoSocietario> rst = new ResponseDTO<TipoSocietario>();
             rst.IsSuccess = false ;

            using (var context = new Entities()) {

                try
                {
                     // Definir la consulta SQL parametrizada para evitar SQL Injection
                //parámetros con sintaxis de Oracle :parametro
                const string sql = "select * from tabgen t where t.tabtipotab = 002 and t.tabclave = :codigo and t.tabclave != '*'";

                // Crear el parámetro usando Oracle.DataAccess.Client.OracleParameter
                OracleParameter codigoParam = new OracleParameter(":codigo", OracleDbType.Varchar2) { Value = codigo };

                // Ejecutar la consulta SQL  
                var tipoSocietarioDB = context.Database.SqlQuery<TABGEN>(sql, codigoParam).FirstOrDefault();
                //verificar si se encontro algun registro
                if (tipoSocietarioDB != null)
                {

                    // Mapear el resultado a TipoSocietario
                    var tipoSocietario = MapToTipoSocietario(tipoSocietarioDB);

                    rst.Data = tipoSocietario ;
                    rst.IsSuccess = true;
                    rst.Message = "OK";
                    
                }}
                catch (Exception ex)
                {
                    
                    rst.Message = ex.Message ;
                }
               
                return rst ;
                
              
            
        }}

        #endregion

        #region Miembros de ITipoSocietarioReposity


        public ResponseDTO<IEnumerable<TipoSocietario>> GetTipoSocietarioPorTipo(string tipo)
        {
            ResponseDTO<IEnumerable<TipoSocietario>> rst = new ResponseDTO<IEnumerable<TipoSocietario>>();
            rst.IsSuccess = false;
            List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();

            using (var context = new Entities())
            {
                try
                {
                    // Convertir tanto el campo como el parámetro a mayúsculas para afinar la búsqueda
                    const string sql = "select * from tabgen t where t.tabtipotab = '002' and UPPER(t.tabconten1) LIKE UPPER(:tipo) and t.tabclave != '*'";

                    // Crear el parámetro usando Oracle.DataAccess.Client.OracleParameter
                    OracleParameter tipoSocParam = new OracleParameter(":tipo", OracleDbType.Varchar2)
                    {
                        Value = "%" + tipo + "%" // Agregar comodines para simular un "contains"
                    };

                    // Ejecutar la consulta SQL pasando el parámetro
                    var tiposSocietariosDB = context.Database.SqlQuery<TABGEN>(sql, tipoSocParam).ToList();

                    // Verificar si la consulta devolvió algún resultado
                    if (tiposSocietariosDB != null)
                    {
                        // Iterar sobre cada elemento de la lista devuelta
                        foreach (var tipoSocietariodb in tiposSocietariosDB)
                        {
                            // Mapear el objeto TABGEN a TipoSocietario
                            TipoSocietario tiposocietario = MapToTipoSocietario(tipoSocietariodb);
                            // Añadir el objeto a la lista final
                            ListaTiposSocietarios.Add(tiposocietario);
                        }

                        // Indicar que la operación fue exitosa y asignar la lista resultante
                        rst.IsSuccess = true;
                        rst.Data = ListaTiposSocietarios;
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según sea necesario
                    rst.Message = "Error al obtener los tipos societarios: " + ex.Message;
                }
            }

           
            return rst;
        }
            
        
        #endregion

        #region Miembros de ITipoSocietarioReposity


     

                    #endregion

        #region Miembros de ITipoSocietarioReposity


        public ResponseDTO<IEnumerable<TipoSocietario>> GetTiposSocietariosCodigosSinCeroALaIzq()
        {
            ResponseDTO<IEnumerable<TipoSocietario>> rst = new ResponseDTO<IEnumerable<TipoSocietario>>();
            rst.IsSuccess = false;
               List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();

            // Usamos el contexto de Entity Framework para interactuar con la base de datos
            using (var context = new Entities())
            {
                try
                {
                    // Definimos la consulta SQL que ejecutará el procedimiento almacenado
                    var sql = "BEGIN PK_API_LEGACY.ListTiposSocietarios(:p_cursor); END;";

                    // Creamos un parámetro para el cursor de salida
                    var outputCursor = new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

                    // Ejecutamos la consulta SQL y obtenemos los resultados en una lista
                    var ListaTipoSocietarioDB = context.Database.SqlQuery<TABGEN>(sql, outputCursor).ToList();

                    // Iteramos sobre cada elemento de la lista devuelta por el procedimiento
                    foreach (var tipoSocietariodb in ListaTipoSocietarioDB)
                    {
                        // Mapeamos el objeto TABGEN_PROD al objeto TipoSocietario
                        TipoSocietario tiposocietario = MapToTipoSocietario(tipoSocietariodb);
                        // Le saco el cero a la izquierda y asigno el resultado a la propiedad
                        tiposocietario.Codigo = tiposocietario.Codigo.TrimStart('0');
                        // Añadimos el objeto TipoSocietario a la lista final
                        ListaTiposSocietarios.Add(tiposocietario);
                        
                    }

                       rst.Data = ListaTiposSocietarios;
                       rst.IsSuccess = true;
                       rst.Message = "OK";
                }
                catch (Exception ex)
                {

                    rst.Message = ex.Message;
                }
             
            }
          
            
             return rst;
        }

        #endregion
    }
       
      
}

#endregion
