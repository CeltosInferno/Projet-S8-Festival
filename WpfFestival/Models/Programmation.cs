using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFestival.Models
{
    class Programmation
    {
        public int ProgrammationId { get; set; }
        public string ProgrammationName { get; set; }
        public int FestivalId { get; set; }
        public int SceneId { get; set; }
        public int ArtisteId { get; set; }
    }
}
