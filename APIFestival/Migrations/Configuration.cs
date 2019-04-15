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


            var Festivals = new List<Festival>
            {
                new Festival{ Name="armada", Description="boat festival", StartDate=DateTime.Parse("2019-06-06"), EndDate=DateTime.Parse("2019-06-16"), LieuName= "Rouen", PostalCode=76100},
                new Festival{ Name="spring", Description="SPRING, festival des nouvelles formes de cirque en Normandie est " +
                "coordonné par la Plateforme 2 Pôles Cirque en Normandie / La Brèche à Cherbourg – Cirque-Théâtre d’Elbeuf " +
                "avec 60 partenaires sur tout le territoire normand.",
                    StartDate =DateTime.Parse("2019-03-01"), EndDate=DateTime.Parse("2019-04-05"), LieuName= "Rouen", PostalCode=76100 },
                new Festival{ Name="musee", Description="UNE ANNEE AU MUSEE", StartDate=DateTime.Parse("2019-04-10"), EndDate=DateTime.Parse("2019-04-17"), LieuName= "Rouen", PostalCode=76100}
            };

            Festivals.ForEach(f => context.Festivals.AddOrUpdate(f1 => f1.Id, f));
            context.SaveChanges();

           

            

            var Artistes = new List<Artiste>
            {
                new Artiste{ ArtisteName="Michael Jackson", Nationality="American",
                    Style ="Rock", MusicExtract="Heal the world", Photo="None"},
                new Artiste{ ArtisteName="Julien Doré", Nationality="French",
                    Style ="Pop", MusicExtract="Coco Câline", Photo="None"}
            };
            Artistes.ForEach(a => context.Artistes.AddOrUpdate(a1 => a1.ArtisteName, a));
            context.SaveChanges();


            var Scenes = new List<Scene>
            {
                new Scene { SceneName="D1269", Capacity=40},
                new Scene { SceneName="B1213", Capacity=50}
            };
            Scenes.ForEach(s => context.Scenes.AddOrUpdate(s1 => s1.SceneName, s));
            context.SaveChanges();
            //AddOrUpdateArtiste(context, 1,1);
            //AddOrUpdateArtiste(context, 1,2);

            //AddOrUpdateScene(context, 1, 1);
            //AddOrUpdateScene(context, 1, 2);

            var Programmations = new List<Programmation>
            {
                new Programmation{ ProgrammationName="a", FestivalId=1, ArtisteId=1, SceneId=1 },
                    //Artistes = new List<Artiste>(),   
                    //Scenes = new List<Scene>()
                
                new Programmation{ ProgrammationName="b", FestivalId=2, ArtisteId=2, SceneId=2 }
                    //Artistes = new List<Artiste>(),
                    //Scenes = new List<Scene>()
                
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
