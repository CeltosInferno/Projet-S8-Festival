namespace APIFestival.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using APIFestival.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<APIFestival.Models.APIFestivalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(APIFestival.Models.APIFestivalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //base.Seed(context);
            var Organisateurs = new List<Organisateur>
            {
                new Organisateur{ Login="111@111.com" , Mdp="111"},
                new Organisateur{ Login = "222@222.com", Mdp="222"}
            };

            Organisateurs.ForEach(o => context.Organisateurs.AddOrUpdate(o1 => o1.Login, o));
            context.SaveChanges();

            var Festivals = new List<Festival>
            {
                new Festival{ Nom="armada", Description="boat festival", DateDebut=DateTime.Parse("2019-06-06"), DateFin=DateTime.Parse("2019-06-16"), Lieu= "Rouen", CodePostal=76100  , IsInscription=true, IsPublication= false, OrganisateurId=1},
                new Festival{ Nom="spring", Description="SPRING, festival des nouvelles formes de cirque en Normandie est " +
                "coordonné par la Plateforme 2 Pôles Cirque en Normandie / La Brèche à Cherbourg – Cirque-Théâtre d’Elbeuf " +
                "avec 60 partenaires sur tout le territoire normand.",
                    DateDebut =DateTime.Parse("2019-03-01"), DateFin=DateTime.Parse("2019-04-05"), Lieu= "Rouen", CodePostal=76100, IsInscription = true , IsPublication= true , OrganisateurId=1, },
                new Festival{ Nom="musee", Description="UNE ANNEE AU MUSEE", DateDebut=DateTime.Parse("2019-04-10"), DateFin=DateTime.Parse("2019-04-17"), Lieu= "Rouen", CodePostal=76100, IsInscription=false, IsPublication= false, OrganisateurId=1}
            };

            Festivals.ForEach(f => context.Festivals.AddOrUpdate(f1 => f1.Id, f));
            context.SaveChanges();

           

            

            var Artistes = new List<Artiste>
            {
                new Artiste{ ArtisteNom="Michael Jackson", Nationality="American",
                    Style ="Rock", MusicExtract="Heal the world", Photo="None"},
                new Artiste{ ArtisteNom="Julien Doré", Nationality="French",
                    Style ="Pop", MusicExtract="Coco Câline", Photo="None"}
            };
            Artistes.ForEach(a => context.Artistes.AddOrUpdate(a1 => a1.ArtisteNom, a));
            context.SaveChanges();


            var Scenes = new List<Scene>
            {
                new Scene { Nom="D1269", Capacite=40},
                new Scene { Nom="B1213", Capacite=50}
            };
            Scenes.ForEach(s => context.Scenes.AddOrUpdate(s1 => s1.Nom, s));
            context.SaveChanges();
            //AddOrUpdateArtiste(context, 1,1);
            //AddOrUpdateArtiste(context, 1,2);

            //AddOrUpdateScene(context, 1, 1);
            //AddOrUpdateScene(context, 1, 2);

            var Programmations = new List<Programmation>
            {
                new Programmation{ ProgrammationName="a", FestivalID=1, ArtisteID=1, SceneID=1, Duration=45, DateDebutConcert=DateTime.Parse("2019-06-06"),OrganisateurID=1, DateFinConcert=DateTime.Parse("2019-06-06")},
                    //Artistes = new List<Artiste>(),   
                    //Scenes = new List<Scene>()
                
                new Programmation{ ProgrammationName="b", FestivalID=2, ArtisteID=2, SceneID=2 , Duration=30, DateDebutConcert=DateTime.Parse("2019-06-06"), OrganisateurID=1, DateFinConcert=DateTime.Parse("2019-06-06")},
                    //Artistes = new List<Artiste>(),
                    //Scenes = new List<Scene>()
                new Programmation{ ProgrammationName="bbb", FestivalID=2, ArtisteID=1, SceneID=2 , Duration= 25, DateDebutConcert = DateTime.Parse("2019-06-06"), OrganisateurID=1, DateFinConcert=DateTime.Parse("2019-06-06")}

            };
            Programmations.ForEach(p => context.Programmations.AddOrUpdate(p1 => p1.ProgrammationName, p));
            context.SaveChanges();
        }
        //void AddOrUpdateArtiste(APIFestivalContext context, int ProgrammationId, int ArtisteId)
        //{
        //    var cps = context.Programmations.SingleOrDefault(p => p.ProgrammationId==ProgrammationId);
        //    var art = cps.Artistes.SingleOrDefault(a => a.Id==ArtisteId);
        //    if (art == null)
        //        cps.Artistes.Add(context.Artistes.Single(a => a.Id==ArtisteId));
        //}

        //void AddOrUpdateScene(APIFestivalContext context, int ProgrammationId, int SceneId)
        //{
        //    var cps = context.Programmations.SingleOrDefault(p => p.ProgrammationId == ProgrammationId);
        //    var sce = cps.Scenes.SingleOrDefault(s => s.SceneId == SceneId);
        //    if (sce == null)
        //        cps.Scenes.Add(context.Scenes.Single(s => s.SceneId == SceneId));
        //}
    }
}
