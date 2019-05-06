using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class FestivalDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateDebut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateFin { get; set; }

        public string Lieu { get; set; }
        public int CodePostal { get; set; }

        public bool IsInscription { get; set; }
        public bool IsPublication { get; set; }

        public int OrganisateurId { get; set; }

        public int UserId { get; set; }
        public float Prix { get; set; }

        public IEnumerable<ProgrammationDTO> ProgrammationsList { get; set; }
    }
}