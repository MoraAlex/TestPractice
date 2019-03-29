using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Servicios.ServiciosDeTerceros
{
    public class ServicioExternoSPEI : IServicioExternoSPEI
    {
        private const string URL = "https://banxico.org/SPEI/Envios";
        private string urlParameters = "?CLABE=";

        public bool EnviarSpei(string banco, string CLABE, decimal cantidad)
        {
            var result = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                var callResult = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                result = Convert.ToBoolean(callResult);
            }
            else
            {
                throw new InvalidOperationException($"{(int)response.StatusCode}, {response.ReasonPhrase}");
            }
            return result;
        }
    }
}
