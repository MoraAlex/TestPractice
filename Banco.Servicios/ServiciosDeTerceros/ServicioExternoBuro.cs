using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Banco.Servicios.ServiciosDeTerceros
{
    public class ServicioExternoBuro : IServicioExternoBuro
    {
        private const string URL = "https://burodecredito.com/buro.json";
        private string urlParameters = "?api_key=123";

        public decimal ConsultarBuro(string RFC)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(URL);
          
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {              
                var callResult = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                return Convert.ToDecimal(callResult);
            }
            else
            {
                throw new InvalidOperationException($"{(int)response.StatusCode}, {response.ReasonPhrase}");
            }            
        }
    }
}
