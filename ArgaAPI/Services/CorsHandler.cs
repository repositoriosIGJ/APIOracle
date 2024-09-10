using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ArgaAPI.Services
{
    public class CorsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Crear la respuesta predeterminada
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            // Verificar si la solicitud contiene la cabecera 'Origin'
            if (request.Headers.Contains("Origin"))
            {
                // Agregar las cabeceras de CORS para ambas URLs
                var origin = request.Headers.GetValues("Origin").FirstOrDefault();
                if (origin == "https://localhost:7276" || origin == "https://localhost:7198")
                {
                    response.Headers.Add("Access-Control-Allow-Origin", origin);
                }

                response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");

                // Si el método es OPTIONS, responder inmediatamente
                if (request.Method == HttpMethod.Options)
                {
                    return Task.FromResult(response);
                }
            }

            // Para todas las demás solicitudes, continuar con el pipeline
            return base.SendAsync(request, cancellationToken).ContinueWith(t =>
            {
                var resp = t.Result;
                if (request.Headers.Contains("Origin"))
                {
                    var origin = request.Headers.GetValues("Origin").FirstOrDefault();
                    if (origin == "https://localhost:7276" || origin == "https://localhost:7198")
                    {
                        resp.Headers.Add("Access-Control-Allow-Origin", origin);
                    }
                }
                return resp;
            });
        }
    }
}