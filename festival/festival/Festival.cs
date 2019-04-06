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
        public string Lieu { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public int Scenes { get; set; }


        public Festival(int id, string nom, DateTime dateDebut, DateTime dateFin, int scenes, string lieu)
        {
            Id = id;
            Nom = nom;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Scenes = scenes;
            Lieu = lieu;
        }


        public Festival(string nom, string lieu)
        {
            Nom = nom;
            Lieu = lieu;
        }


        public override String ToString()
        {
            return "Festival "+ Nom + "créé" ;
        }

    }
}
