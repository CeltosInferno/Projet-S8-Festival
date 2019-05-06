using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.WEB
{
    public class InteretWEB
    {
        public int Id { get; set; }
        public int Interesser { get; set; }

        public int FestivalId { get; set; }
        public virtual FestivalWEB Festival { get; set; }
    }
}