using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FestivalApi.Models
{
    public class Programmation
    {
        public int ProgrammationId { get; set; }

        [ForeignKey("Festival")]
        public int FestivalID { get; set; }
        public virtual Festival Festival { get; set; }

        [ForeignKey("Artiste")]
        public int ArtisteID { get; set; }
        public virtual Artiste Artiste { get; set; }

        [ForeignKey("Scene")]
        public int SceneID { get; set; }
        public virtual Scene Scene { get; set; }

        [ForeignKey("Organisateur")]
        public int OrganisateurID { get; set; }
        public virtual Organisateur Organisateur { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDebutConcert { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateFinConcert { get; set; }
    }
}