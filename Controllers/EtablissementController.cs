using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hospitalidée_CRM_Back_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hospitalidée_CRM_Back_End.Controllers
{
    [ApiController]
    [Route("Etablissement/{siret?}")]
    public class EtablissementController : ControllerBase
    {
        [HttpGet]
        public EtablissementResponse GetEtablissement(string? siret)
        {
            if(siret == null)
            {
                return null;
            }
            GovernmentApiJson jsonGovernment = RetrieveEtablissementFromGovernment(siret);
            if(jsonGovernment == null)
            {
                return null;
            }
            EtablissementResponse etablissementResponse = ConvertIntoResponse(jsonGovernment);
            return etablissementResponse;
        }

        private static EtablissementResponse ConvertIntoResponse(GovernmentApiJson jsonGovernment)
        {
            EtablissementResponse etablissementResponse = new EtablissementResponse();
            UniteLegaleResponse uniteLegaleResponse = new UniteLegaleResponse();
            etablissementResponse.unite_legale = uniteLegaleResponse;
            
            etablissementResponse.libelle_commune = jsonGovernment.etablissement.libelle_commune;
            etablissementResponse.siret = jsonGovernment.etablissement.siret;
            etablissementResponse.numero_voie = jsonGovernment.etablissement.numero_voie;
            etablissementResponse.activite_principale = jsonGovernment.etablissement.activite_principale;
            etablissementResponse.code_postal = jsonGovernment.etablissement.code_postal;
            etablissementResponse.libelle_voie = jsonGovernment.etablissement.libelle_voie;
            etablissementResponse.type_voie = jsonGovernment.etablissement.type_voie;
            etablissementResponse.denomination_usuelle = jsonGovernment.etablissement.denomination_usuelle;
            uniteLegaleResponse.denomination = jsonGovernment.etablissement.unite_legale.denomination;
            uniteLegaleResponse.nom = jsonGovernment.etablissement.unite_legale.nom;
            uniteLegaleResponse.prenom_usuel = jsonGovernment.etablissement.unite_legale.prenom_usuel;
            uniteLegaleResponse.nomenclature_activite_principale = jsonGovernment.etablissement.unite_legale.nomenclature_activite_principale;
            uniteLegaleResponse.siren = jsonGovernment.etablissement.unite_legale.siren;
            
            return etablissementResponse;
        }

        readonly HttpClient client = new HttpClient();
        
        private GovernmentApiJson RetrieveEtablissementFromGovernment(string siret)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = client.GetAsync("https://entreprise.data.gouv.fr/api/sirene/v3/etablissements/" + siret).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                GovernmentApiJson JsonDeserializer = JsonSerializer.Deserialize<GovernmentApiJson>(responseBody);

                return JsonDeserializer;
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

