﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Models;

namespace ArgaAPI.Repositorio.Contrato
{
    public interface IDatosCivilesRepository
    {
        IEnumerable<DatosCivilesDTO> GetDatosCiviles(DatosCivilesRequest datosCivilesReq);
    }
}