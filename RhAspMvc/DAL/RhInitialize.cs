using RhAspMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RhAspMvc.DAL
{
    public class RhInitialize : DropCreateDatabaseIfModelChanges<RhContext>
    {
        protected override void Seed(RhContext context)
        {
            var services = new List<Service>
            {
                new Service {Libelle = "Orl"},
                new Service {Libelle = "Urologie"},
                new Service {Libelle = "Urgence"}
            };

            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();

            var specialites = new List<Specialite>
            {
                new Specialite {Libelle = "Specialite1",Service = services.ElementAt(0)},
                new Specialite {Libelle = "Specialite2",Service = services.ElementAt(0)},
                new Specialite {Libelle = "Specialite3",Service = services.ElementAt(1)},
                new Specialite {Libelle = "Specialite4",Service = services.ElementAt(1)},
                new Specialite {Libelle = "Specialite5",Service = services.ElementAt(2)},
                new Specialite {Libelle = "Specialite6",Service = services.ElementAt(2)}
            };

            specialites.ForEach(sp => context.Specialites.Add(sp));
            context.SaveChanges();

            var medecins = new List<Medecin>
            {
                new Medecin {DateNaiss = DateTime.Parse("15/02/2000"),Matricule = "001",Nom = "Thera",Prenom = "Daouda", Salaire = 1000000, Service = services.ElementAt(0), Specialites = new List<Specialite>{specialites.ElementAt(0)} },
                new Medecin {DateNaiss = DateTime.Parse("10/03/2002"),Matricule = "002",Nom = "Napal",Prenom = "Ousman", Salaire = 2000000, Service = services.ElementAt(1), Specialites = new List<Specialite>{specialites.ElementAt(2), specialites.ElementAt(3) } },
                new Medecin {DateNaiss = DateTime.Parse("06/05/1999"),Matricule = "003",Nom = "Karou",Prenom = "Diallo", Salaire = 1005000, Service = services.ElementAt(2), Specialites = new List<Specialite>{specialites.ElementAt(4), specialites.ElementAt(5) } }
            };

            medecins.ForEach(m => context.Medecins.Add(m));
            context.SaveChanges();
        }
    }
}