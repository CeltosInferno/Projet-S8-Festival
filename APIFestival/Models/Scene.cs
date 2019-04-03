using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models
{
    public class Scene
    {
        public Scene()
        {
            this.Programmations = new HashSet<Programmation>();
        }

        public int SceneId { get; set; }
        public string SceneName { get; set; }
        public int Capacity { get; set; }
        
        public virtual ICollection<Programmation> Programmations { get; set; }
    }
}