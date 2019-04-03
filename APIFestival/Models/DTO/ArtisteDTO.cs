using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class ArtisteDTO
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Photo { get; set; }
        public string Style { get; set; }
        public string Comment { get; set; }
        public string Nationality { get; set; }
        public string MusicExtract { get; set; }
        public int ProgrammationId { get; set; }


    }
}