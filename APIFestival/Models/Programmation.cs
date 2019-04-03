using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models
{
    public class Programmation
    {
        public Programmation()
        {
            this.Artistes = new HashSet<Artiste>();
            this.Scenes = new HashSet<Scene>();
        }
        public int ProgrammationId { get; set; }
        public string ProgrammationName { get; set; }
        

        public int FestivalId { get; set; }
        public Festival Festival { get; set; }

        public virtual ICollection<Artiste> Artistes { get; set; }
        public virtual ICollection<Scene> Scenes { get; set; }
    }
}