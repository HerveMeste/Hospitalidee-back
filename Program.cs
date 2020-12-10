using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospitalidée_CRM_Back_End.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Hospitalidée_CRM_Back_End
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AddDatabase();
            CreateHostBuilder(args).Build().Run();
        }
        private static void AddDatabase()
        {
            using (var context = new EtablissementContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Etablissement etablissement = new Etablissement()
                {
                    activite_principale = "85.59A",
                    denomination_usuelle = "WILD CODE SCHOOL",
                    siret = "79492606300098",
                    numero_voie = "11",
                    type_voie = "RUE",
                    libelle_voie = "DE POISSY",
                    code_postal = "75005",
                    libelle_commune = "Paris",
                    unite_legale = new UniteLegale()
                    {
                        prenom_usuel = "",
                        nom = "",
                        siren = "794926063",
                        denomination = "INNOV'EDUC",
                        nomenclature_activité_principale = "NAFRev2",

                    }
                };
                Etablissement etablissement2 = new Etablissement()
                {
                    activite_principale = "86.22C",
                    denomination_usuelle = "",
                    siret = "38244544300023",
                    numero_voie = "1",
                    type_voie = "CRS",
                    libelle_voie = "DU 14 JUILLET",
                    code_postal = "47000",
                    libelle_commune = "AGEN",
                    unite_legale = new UniteLegale()
                    {
                        prenom_usuel = "CHARLES",
                        nom = "BANUS",
                        siren = "382445443",
                        denomination = "",
                        nomenclature_activité_principale = "NAFRev2",

                    }
                };
                context.AddRange(etablissement, etablissement2);
                context.SaveChanges();


            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }


}
