using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace APIFestival.Models.DAL
{
    public class APIFestivalContextInitializer: CreateDatabaseIfNotExists<APIFestivalContext>
    {
        protected override void Seed(APIFestivalContext context)
        {
            

            var festivals = new List<Festival>
            {
                new Festival{ Name="armada", Description="boat festival", StartDate=DateTime.Parse("2019-06-06"), EndDate=DateTime.Parse("2019-06-16")},
                new Festival{ Name="spring", Description="SPRING, festival des nouvelles formes de cirque en Normandie est " +
                "coordonné par la Plateforme 2 Pôles Cirque en Normandie / La Brèche à Cherbourg – Cirque-Théâtre d’Elbeuf " +
                "avec 60 partenaires sur tout le territoire normand.",
                    StartDate =DateTime.Parse("2019-03-01"), EndDate=DateTime.Parse("2019-04-05")},
                new Festival{ Name="armada", Description="UNE ANNEE AU MUSEE", StartDate=DateTime.Parse("2019-04-10"), EndDate=DateTime.Parse("2019-04-17")}
            };

            festivals.ForEach(f => context.Festivals.Add(f));
            context.SaveChanges();

            var Lieux = new List<Lieu>
            {
                new Lieu{ LieuName="Roeun", PostalCode=76000},
                new Lieu{ LieuName="Paris", PostalCode=75001}
            };

            Lieux.ForEach(l => context.Lieux.Add(l));
            context.SaveChanges();

            var Artistes = new List<Artiste>
            {
                new Artiste{ FirstName="Michael", LastName="Jackson", Nationality="American", Style="Rock", MusicExtract="Heal the world", Photo="None"},
                new Artiste{ FirstName="Julien", LastName="Doré", Nationality="French", Style="Pop", MusicExtract="Coco Câline", Photo="None"}
            };

            Artistes.ForEach(a => context.Artistes.Add(a));
            context.SaveChanges();

            var Scenes = new List<Scene>
            {
                new Scene { SceneName="D1269", Capacity=40},
                new Scene { SceneName="B1213", Capacity=50}
            };
            Scenes.ForEach(s => context.Scenes.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}