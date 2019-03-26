using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festival
{
    public class InitBdd
    {
        public void Init()
        {
            IDatabaseInitializer<BddContext> init = new CreateDatabaseIfNotExists<BddContext>();
            //IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            //IDatabaseInitializer<BddContext> init = new DropCreateDatabaseIfModelChanges<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());
        }
        public void AddDatas()
        {

        }
    }
}
