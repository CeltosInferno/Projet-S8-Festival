
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFestival.Models
{
    public class Scene
    {
        public int SceneId { get; set; }
        public string SceneName { get; set; }
        public int Capacity { get; set; }
        
        
        public virtual ICollection<Programmation> Programmations { get; set; }
    }
}