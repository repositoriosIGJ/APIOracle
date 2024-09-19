﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Business.Contrato;
using ArgaAPI.Models;
using ArgaAPI.Repositorio.Contrato;

namespace ArgaAPI.Business.Implementacion
{
    public class DatosCivilesBusiness : IDatosCivilesBusiness
    {

        private readonly IDatosCivilesRepository _datosCivilesRepository;

        public DatosCivilesBusiness(IDatosCivilesRepository datosCivilesRepository)
        {

            _datosCivilesRepository = datosCivilesRepository;

        }



        public IEnumerable<DatosCivilesDTO> GetDatosCiviles(DatosCivilesRequest datosCivilesReq)
        {
            var DatosCiviles = _datosCivilesRepository.GetDatosCiviles(datosCivilesReq);

            return DatosCiviles;
        }
    }
}