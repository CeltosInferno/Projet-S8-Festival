using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festival
{
    public class Festival
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Scenes { get; set; }


        public Festival(int id, string nom, DateTime dateDebut, DateTime dateFin, string scenes)
        {
            Id = id;
            Nom = nom;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Scenes = scenes;
        }


    }
}
