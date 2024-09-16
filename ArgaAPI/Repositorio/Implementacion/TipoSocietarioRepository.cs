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

        public IEnumerable<TipoSocietario> GetTiposSocietarios()
        {
            List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();

            // Usamos el contexto de Entity Framework para interactuar con la base de datos
            using (var context = new DEVIGJ())
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
            }
            // Retornamos la lista de tipos societarios
            return ListaTiposSocietarios;
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


        public TipoSocietario GetTipoSocietarioPorCodigo(string codigo)
        {
            using (var context = new Entities()) {

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

                    return tipoSocietario;

                }
                else { 
                
                    return null ;
                }
            }
        }

        #endregion

        #region Miembros de ITipoSocietarioReposity


        public IEnumerable<TipoSocietario> GetTipoSocietarioPorTipo(string tipo)
        {
            List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();
            using (var context = new Entities())
            {
                // Convertir tanto el campo como el parámetro a mayúsculas para afinar la busqueda dado a la DB es sensible a mayusculas y min y estan en mayusculas
                const string sql = "select * from tabgen t where t.tabtipotab = '002' and UPPER(t.tabconten1) LIKE UPPER(:tipo) and t.tabclave != '*'";
                
                // Crear el parámetro usando Oracle.DataAccess.Client.OracleParameter
                OracleParameter tipoSocParam = new OracleParameter(":tipo", OracleDbType.Varchar2)
                {
                    Value = "%" + tipo + "%" // Agregar comodines para simular un "contains"
                };
                // Ejecutar la consulta SQL pasando el parámetro
                var tiposSocietariosDB = context.Database.SqlQuery<TABGEN>(sql, tipoSocParam);

                if (tiposSocietariosDB != null)
                {
                         // Iteramos sobre cada elemento de la lista devuelta por el procedimiento
                    foreach (var tipoSocietariodb in tiposSocietariosDB)
                    {
                        // Mapeamos el objeto TABGEN_PROD al objeto TipoSocietario
                        TipoSocietario tiposocietario = MapToTipoSocietario(tipoSocietariodb);

                        // Añadimos el objeto TipoSocietario a la lista final
                        ListaTiposSocietarios.Add(tiposocietario);
                    }

                    return ListaTiposSocietarios;
                }
                else
                {
                    return null;
                }
            }

        }
        #endregion

        #region Miembros de ITipoSocietarioReposity


     

                    #endregion

        #region Miembros de ITipoSocietarioReposity


        public IEnumerable<TipoSocietario> GetTiposSocietariosCodigosSinCeroALaIzq()
        {
            

               List<TipoSocietario> ListaTiposSocietarios = new List<TipoSocietario>();

            // Usamos el contexto de Entity Framework para interactuar con la base de datos
            using (var context = new Entities())
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
            }
            // Retornamos la lista de tipos societarios
            return ListaTiposSocietarios;
        }

        #endregion
    }
       
      
}

#endregion
