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
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public string LieuName { get; set; }
        public int PostalCode { get; set; }

        public bool IsInscription { get; set; }
        public bool IsPublication { get; set; }

        public int OrganisateurId { get; set; }

        public int NbSeats { get; set; }
        public float Price { get; set; }

        public IEnumerable<ProgrammationDTO> ProgrammationsList { get; set; }
    }
}