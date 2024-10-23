﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArgaAPI.Repositorio.Contrato;
using ArgaAPI.Data;
using Newtonsoft.Json;
using ArgaAPI.Models;

using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;

namespace ArgaAPI.Repositorio.Implementacion
{
    public class DatosCivilesRepository : IDatosCivilesRepository
    {

        public IEnumerable<DatosCivilesDTO> GetDatosCiviles(DatosCivilesRequest datosCivilesReq)
        {
            List<DatosCivilesDTO> ListaDatosCiviles = new List<DatosCivilesDTO>();
            using (var context = new Entities())
            {

                string personeria = @"SELECT t.tracodtram,c.cdtdesctram,t.trafechact,t.tranrocorr,e.exprazonso,
e.exptiposoc,t.tranrotram,d.dtrcoddest,d.dtrfechart,t.trafecreg
FROM tramit t, exptes e, cod_tram c, destra d
WHERE t.tranrocorr = e.expnrocorr
AND t.tracodtram = c.cdtcodtram
AND t.tranrocorr = d.dtrnrocorr
AND t.tracodtram = d.dtrcodtram
AND t.tranrotram = d.dtrnrotram
AND d.dtrfechast IS NULL
AND t.tracodtram IN ('00032','00054','00059','00070','00071','00072','00073','00079','00080','00081','00084','00085',
'00089','00104','00110','00111','00120','00121','00137','00139','00140','00141','00142','00143','00144','00145','00146',
'00147','00149','00151','00176','00180','00181','00182','00183','00201','00203','00204','00225','00251','00281','00290',
'00291','00298','00300','00301','00302','00305','00309','00343','00350','00351','00353','00361','00362','00363','00364',
'00365','00366','00367','00372','00373','00377','00391','00403','00407','00412','00414','00420','00421','00431','00467',
'00471','00478','00480','00481','00482','00483','00484','00486','00489','00500','00505','00507','00508','00510','00511',
'00512','00521','00528','00530','00531','00541','00542','00545','00547','00549','00550','00559','00575','00578','00590',
'00591','00595','00620','00633','00634','00648','00656','00664','00680','00691','00693','00695','00697','00706','00707',
'00712','00714','00720','00773','00782','00784','00809','00811','00812','00813','00815','00821','00884','00891','00940',
'00943','01000','01008','01019','01020','01021','01022','01024','01026','01028','01029','01034','01036','01038','01042',
'01071','01090','01091','01095','01108','01146','01148','01181','01201','01272','01275','01291','01301','01362','01370',
'01409','01410','01415','01430','01431','01433','01480','01500','01501','01507','01511','01521','01541','01542','01544',
'01547','01564','01566','01571','01663','01665','01752','02000','02003','02071','02081','02085','02111','02121','02141',
'02143','02147','02149','02201','02290','02304','02309','02311','02343','02351','02377','02414','02431','02502','02595',
'02597','02720','03003','03019','03020','03021','03028','03140','03201','03203','03204','03421','03481','03482','03483',
'03484','03489','03511','03528','03545','03547','03549','03595','03720','03782','03811','04095','04121','04147','04182',
'04201','04290','04351','04370','04502','04595')
AND t.trafechact BETWEEN :FechaInicio AND :FechaFinal
AND e.exptiposoc BETWEEN 99 AND 141
AND t.tragrptram IS NULL
ORDER BY t.trafechact
";




                string fiscalizacion = @"
    SELECT t.tracodtram, c.cdtdesctram, t.trafechact, t.tranrocorr, e.exprazonso, e.exptiposoc, 
           t.tranrotram, d.dtrcoddest, d.dtrfechart, t.trafecreg 
FROM tramit t, exptes e, cod_tram c, destra d 
WHERE t.tranrocorr = e.expnrocorr 
      AND t.tracodtram = c.cdtcodtram 
      AND t.tranrocorr = d.dtrnrocorr 
      AND t.tracodtram = d.dtrcodtram 
      AND t.tranrotram = d.dtrnrotram 
      AND d.dtrfechast IS NULL 
      AND t.tracodtram IN (
          '00020','00027','00045','00106','00270','00630','00710','00740','00745','00750',
          '00870','00902', '00930','00931','00950','00960','00970','01030','01060','01061',
          '01070','01100','01110','01270','01273','01290','01320', '01330','01450','01462',
          '01588','01610','01670','01800','01820','03000','03710','04000','04001','04002',
          '04003','07000'
      ) 
      AND t.trafechact BETWEEN :FechaInicio AND :FechaFinal 
      AND e.exptiposoc BETWEEN 99 AND 141 
      AND t.tragrptram IS NULL 
    ORDER BY t.trafechact";

                string contable = @"SELECT t.tracodtram,c.cdtdesctram,t.trafechact,t.tranrocorr,e.exprazonso,
e.exptiposoc,t.tranrotram,d.dtrcoddest,d.dtrfechart, t.trafecreg FROM tramit t, exptes e, cod_tram c, destra d
WHERE t.tranrocorr = e.expnrocorr
AND t.tracodtram = c.cdtcodtram
AND t.tranrocorr = d.dtrnrocorr
AND t.tracodtram = d.dtrcodtram
AND t.tranrotram = d.dtrnrotram
AND d.dtrfechast IS NULL
AND t.tracodtram IN ('00030','00131','00160','00161','00164','00169','00920','00921','00922','00925','01169','01170',
'01810','01830','01902','01903','01904','02900','03131','03161','03500','04925')
AND t.trafechact BETWEEN :FechaInicio AND :FechaFinal
AND e.exptiposoc BETWEEN 99 AND 141
AND t.tragrptram IS NULL
ORDER BY t.trafechact
";

                var listaDatosCivilesDB = new List<DatosCiviles>();

                if (datosCivilesReq.Tipo == 1)
                {
                    listaDatosCivilesDB = context.Database.SqlQuery<DatosCiviles>(fiscalizacion, new OracleParameter("FechaInicio", datosCivilesReq.FechaInicio),
             new OracleParameter("FechaFinal", datosCivilesReq.FechaFinal)).ToList();
                }
                else if (datosCivilesReq.Tipo == 2)
                {
                    listaDatosCivilesDB = context.Database.SqlQuery<DatosCiviles>(contable, new OracleParameter("FechaInicio", datosCivilesReq.FechaInicio),
             new OracleParameter("FechaFinal", datosCivilesReq.FechaFinal)).ToList();
                }
                else
                {
                    listaDatosCivilesDB = context.Database.SqlQuery<DatosCiviles>(personeria,
                      new OracleParameter("FechaInicio", datosCivilesReq.FechaInicio),
             new OracleParameter("FechaFinal", datosCivilesReq.FechaFinal)).ToList();
                }

                foreach (var datoCivilDB in listaDatosCivilesDB)
                {
                    DatosCivilesDTO datosCiviles = MapToDatosCivilesDTO(datoCivilDB);
                    ListaDatosCiviles.Add(datosCiviles);
                }

                return ListaDatosCiviles;
            }
        }

        public DatosCivilesDTO MapToDatosCivilesDTO(DatosCiviles datoscivilesdb)
        {

            DatosCivilesDTO datosCiviles = new DatosCivilesDTO()
            {
                Denominacion = datoscivilesdb.EXPRAZONSO,
                Tramite = datoscivilesdb.TRACODTRAM,
                Destino = datoscivilesdb.DTRCODDEST,
                Correlativo = datoscivilesdb.TRANROCORR,
                Alta = datoscivilesdb.TRAFECHACT,
                Pase = datoscivilesdb.DTRFECHART,
                Registracion = datoscivilesdb.TRAFECREG,
                Descripcion = datoscivilesdb.CDTDESCTRAM

            };

            return datosCiviles;
        }
    }
}