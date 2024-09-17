using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Data.DEVIGJ;
using ArgaAPI.DTOs;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class DestinoRepository : IDestinoRepository
    {
        #region Miembros de IDestinoRepository

        public  ResponseDTO<List<Destino>> GetDestinos()
        {
            ResponseDTO<List<Destino>> rst = new ResponseDTO<List<Destino>>();
            rst.IsSuccess = false;
            List<Destino> Destinos = new List<Destino>();
            using (var context = new DEVIGJ())
            {

                try
                {
                    // Definir la consulta SQL 
                    const string sql = "select * from destin t order by t.descoddest";

                    // Ejecutar la consulta SQL  
                    var destinosDB = context.Database.SqlQuery<DESTIN>(sql).ToList();
                    //verificar si se encontro algun registro
                    if (destinosDB != null)
                    {

                        foreach (var destinodb in destinosDB)
                        {

                            // Mapear el resultado a destino y agregarloo a la lista Destinos
                            var destino = MapToDestino(destinodb);

                            Destinos.Add(destino);

                        }
                        rst.Data = Destinos;
                        rst.IsSuccess = true;
                        rst.Message = "OK";
                    }
                  
                }
                catch (Exception ex)
                {
                    rst.Message = ex.Message;
                }

                return rst;
            }
        }

        #endregion

        public Destino MapToDestino(DESTIN destin)
        {

            Destino destino = new Destino()
            {

                Codigo = destin.DESCODDEST,
                Departamento = destin.DESDESCRIP1,
                Area = destin.DESDESCRIP2

            };

            return destino;
        }
    }
}