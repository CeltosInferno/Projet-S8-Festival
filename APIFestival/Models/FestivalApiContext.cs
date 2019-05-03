using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FestivalApi.Models
{
    public class FestivalApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public FestivalApiContext() : base("name=FestivalApiContext")
        {
        }

        public System.Data.Entity.DbSet<FestivalApi.Models.Festival> Festivals { get; set; }

        public System.Data.Entity.DbSet<FestivalApi.Models.Artiste> Artistes { get; set; }

        public System.Data.Entity.DbSet<FestivalApi.Models.Festivalier> Festivaliers { get; set; }

        public System.Data.Entity.DbSet<FestivalApi.Models.Organisateur> Organisateurs { get; set; }

        public System.Data.Entity.DbSet<FestivalApi.Models.Programmation> Programmations { get; set; }

        public System.Data.Entity.DbSet<FestivalApi.Models.Scene> Scenes { get; set; }

        public System.Data.Entity.DbSet<FestivalApi.Models.Selection> Selections { get; set; }
    }
}