
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFestival.Models
{
    public class Scene
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Capacite { get; set; }
        public string Accessibilite { get; set; }
        
        
        public virtual ICollection<Programmation> Programmations { get; set; }
    }
}