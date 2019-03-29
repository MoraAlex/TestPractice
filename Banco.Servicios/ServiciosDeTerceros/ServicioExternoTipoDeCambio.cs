using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Banco.Entidades;

namespace Banco.Servicios.ServiciosDeTerceros
{
    public class ServicioExternoTipoDeCambio
    {
        private const string URL = "https://banxico.org/TipoDeCambio";
        private string urlParameters = "?divisa=";

        public decimal TipoDeCambio(Divisa origen, Divisa destino)
        {
            decimal result = 0;
            var client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"{urlParameters}{origen.ToString()}").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                var callResult = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                result = Convert.ToDecimal(callResult);
            }
            else
            {
                throw new InvalidOperationException($"{(int)response.StatusCode}, {response.ReasonPhrase}");
            }
            return result;
        }
    }
}
