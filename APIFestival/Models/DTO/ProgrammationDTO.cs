using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class ProgrammationDTO
    {
        public int ProgrammationId { get; set; }
        public string ProgrammationName { get; set; }
        public int ArtisteId { get; set; }
        public int FestivalId { get; set; }
        public int SceneId { get; set; }
    }
}