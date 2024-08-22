using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;

namespace ArgaAPI.Business.Implementacion
{
    public class TipoSocietarioBusiness : ITipoSocietarioBusiness
    {
        private readonly ITipoSocietarioReposity _tipoSocietarioRepository;
      

         public TipoSocietarioBusiness(ITipoSocietarioReposity tipoSocietarioRepository){

             _tipoSocietarioRepository = tipoSocietarioRepository;
     
         }

         
        /*
        public IEnumerable<Data.TABGEN_PROD> GetTipoSocietario()
        {
           var TiposSocietarios = _tipoSocietarioRepository.GetTipoSocietario();

               return TiposSocietarios;
        }*/


        public IEnumerable<TipoSocietario> GetTiposSocietarios()
        {
            var TiposSocietarios = _tipoSocietarioRepository.GetTiposSocietarios();

            return TiposSocietarios;
        }


    }
}