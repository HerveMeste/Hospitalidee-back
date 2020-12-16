using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hospitalidée_CRM_Back_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hospitalidée_CRM_Back_End.Controllers
{
    [ApiController]
    [Route("Siren/{siren?}")]
    public class SirenController : ControllerBase
    {
        [HttpGet]
        public GovernmentApiJson GetEtablissement(string? siren)
        {
            if(siren == null)
            {
                return null;
            }
            
            GovernmentApiJson jsonGovernment = RetrieveEtablissementFromGovernment(siren);
            if(jsonGovernment == null)
            {
                return null;
            }
            return jsonGovernment;
        }

        readonly HttpClient client = new HttpClient();

        private GovernmentApiJson RetrieveEtablissementFromGovernment(string siren)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = client.GetAsync("https://entreprise.data.gouv.fr/api/sirene/v3/unites_legales/" + siren).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                GovernmentApiJson jsonGovernmentApi = JsonSerializer.Deserialize<GovernmentApiJson>(responseBody);
                
                return jsonGovernmentApi;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

        }
    }
}
