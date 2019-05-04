using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIFestival.Models
{
    public class Programmation
    {
        
        public int ProgrammationId { get; set; }
        public string ProgrammationName { get; set; }
        
        [ForeignKey("Festival")]
        public int FestivalId { get; set; }
        
        [ForeignKey("Scene")]
        public int SceneId { get; set; }
        [ForeignKey("Artiste")]
        public int ArtisteId { get; set; }
        public DateTime Date { get; set; }
        //public int Hour { get; set; }
        //public int Minute { get; set; }
        public int Duration { get; set; }

        public virtual Festival Festival { get; set; }
        public virtual Artiste Artiste { get; set; }
        public virtual Scene Scene { get; set; }
    }
}