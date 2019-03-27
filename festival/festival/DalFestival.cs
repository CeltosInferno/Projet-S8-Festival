using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festival
{
    public class DalFestival : IDalFestival
    {
        private BddContext bdd;

        public DalFestival()
        {
            bdd = new BddContext();
        }

        public void CreateFestival(Festival festival)
        {
            bdd.Festivals.Add(festival);
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public Festival GetFestival(string nom)
        {
            return bdd.Festivals.FirstOrDefault(festival => festival.Nom.Equals(nom));
        }
        
    }
}
