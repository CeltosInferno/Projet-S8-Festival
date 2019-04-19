using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFestival.Models;
using Prism.Mvvm;
using Prism.Commands;
using System.Net.Http.Headers;
using System.Net.Http;

namespace WpfFestival.ViewModels
{
    public class ModifierArtisteViewModel : BindableBase
    {
        #region Members
        private Artiste _artiste;
        private List<Artiste> _artistesList;
        private bool _isEnabled;
        #endregion

        #region Properties
        public Artiste Artiste
        {
            get { return _artiste; }
            set { SetProperty(ref _artiste, value); }
        }

        public List<Artiste> ArtistesList
        {
            get { return _artistesList; }
            set { SetProperty(ref _artistesList, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        public List<string> StylesList { get; set; }
        public List<string> NationalitiesList { get; set; }
        #endregion

        public ModifierArtisteViewModel()
        {
            
            ModifierArtiste = new DelegateCommand(Executed).ObservesCanExecute(() => IsEnabled);
            ArtistesList = new List<Artiste>();
            Artiste = new Artiste();
            this.GetArtistesList();
            InitialStyles();
            InitialNationalities();

        }
        #region Commands
        public DelegateCommand ModifierArtiste { get; private set; }
        private void Executed()
        {
           
            if (PutArtiste($"api/Artistes/{Artiste.ArtisteId}"))
            {
                
            }
            
        }
        #endregion

        #region Methods
        public void InitialStyles()
        {
            StylesList = new List<string>();
            StylesList.Add("Blue");
            StylesList.Add("Folk");
            StylesList.Add("Rock");
            StylesList.Add("Pop");
        }

        public void InitialNationalities()
        {
            NationalitiesList = new List<string>();
            NationalitiesList.Add("French");
            NationalitiesList.Add("American");
            NationalitiesList.Add("Chinese");
            NationalitiesList.Add("Japanses");
            NationalitiesList.Add("German");
        }

        public void GetArtistesList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Artistes").Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<List<Artiste>>();
                readTask.Wait();
                foreach (Artiste a in readTask.Result)
                {
                    this.ArtistesList.Add(a);
                }
            }
        }
        public bool PutArtiste(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Task<HttpResponseMessage> response = client.PutAsJsonAsync<Artiste>(uri, Artiste);
            response.Wait();

            if (response.Result.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }
        #endregion
    }
}
