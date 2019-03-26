using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festival
{
    public interface IDalFestival : IDisposable
    {
        
        void CreateFestival(Festival festival);
        
        Festival GetFestival(string nom);
       
    }
}
