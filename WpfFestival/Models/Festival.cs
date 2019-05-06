using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfFestival.Models
{
    public class Festival : BindableBase
    {
        private int _id;
        public int Id { get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _festivalNom;
        public string Nom
        {
            get { return _festivalNom; }
            set { SetProperty(ref _festivalNom, value); }
        }

        private DateTime _dateDebut;
        public DateTime DateDebut
        {
            get { return _dateDebut; }
            set { SetProperty(ref _dateDebut, value); }
        }

        private DateTime _dateFin;
        public DateTime DateFin
        {
            get { return _dateFin; }
            set { SetProperty(ref _dateFin, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _lieuName;
        public string Lieu
        {
            get { return _lieuName; }
            set { SetProperty(ref _lieuName, value); }
        }

        private int _postalCode;
        public int CodePostal
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }
        }

        private bool _isPublication;
        public bool IsPublication
        {
            get { return _isPublication; }
            set { SetProperty(ref _isPublication, value); }
        }

        private bool _isInscription;
        public bool IsInscription
        {
            get { return _isInscription; }
            set { SetProperty(ref _isInscription, value); }
        }

        private List<Programmation> _programmationsList;
        public List<Programmation> ProgrammationsList
        {
            get { return _programmationsList; }
            set { SetProperty(ref _programmationsList, value); }
        }

        private int _organisateurId;
        public int OrganisateurId
        {
            get { return _organisateurId; }
            set { SetProperty(ref _organisateurId, value); }
        }

        private int _nbSeats;
        public int NbSeats
        {
            get { return _nbSeats; }
            set { SetProperty(ref _nbSeats, value); }
        }

        private float _price;
        public float Prix
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
    }
}
