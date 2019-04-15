using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class SceneDTO
    {
        public int SceneId { get; set; }
        public string SceneName { get; set; }
        public int Capacity { get; set; }

        public IEnumerable<ProgrammationDTO> Programmations { get; set; }
    }
}