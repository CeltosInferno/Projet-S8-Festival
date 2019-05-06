using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APIFestival.Models
{
    public class APIFestivalContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public APIFestivalContext() : base("name=APIFestivalContext")
        {
        }

        public System.Data.Entity.DbSet<APIFestival.Models.Festivalier> Festivaliers { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Artiste> Artistes { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Festival> Festivals { get; set; }

        //public System.Data.Entity.DbSet<APIFestival.Models.Lieu> Lieux { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Programmation> Programmations { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Scene> Scenes { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Organisateur> Organisateurs { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Selection> Selections { get; set; }

        public System.Data.Entity.DbSet<APIFestival.Models.Interet> Interets { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
