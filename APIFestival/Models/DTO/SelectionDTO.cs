using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class SelectionDTO
    {
        public int SelectionId { get; set; }

        public int ProgrammationId { get; set; }

        public int FestivalierId { get; set; }

        public int PrimaireSecondaire { get; set; }
       


    }
}