using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFestival.Models;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using System.Net.Http.Headers;
using System.Net.Http;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class ModifierArtisteViewModel : BindableBase
    {
        #region Members
        private Artiste _artiste;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public Artiste Artiste
        {
            get { return _artiste; }
            set { SetProperty(ref _artiste, value); }
        }

        
       
        public List<string> StylesList { get; set; }
        public List<string> NationalitiesList { get; set; }
        #endregion
        #region Commands
        public DelegateCommand<string> ModifierArtiste { get; private set; }
        public DelegateCommand<string> GoToGestionArtiste { get; private set; }
        private void ExecutedA(string uri) // Modifier artiste
        {

            if (PutArtiste($"api/Artistes/{Artiste.ArtisteID}"))
            {
                NotificationRequest.Raise(new Notification { Content = "Modifié !!!", Title = "Notification" });
                if (uri != null)
                {
                    _regionManager.RequestNavigate("ContentRegion", uri);
                    _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste
                }
                   

            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Erreur serveur !!!", Title = "Notification" });
            }
        }
       
        private void ExecutedB(string uri) // GOTOGestionArtiste
        {
            if (uri != null)
                _regionManager.RequestNavigate("ContentRegion", uri);
        }
       
        #endregion
        public ModifierArtisteViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            NotificationRequest = new InteractionRequest<INotification>();
            _eventAggregator.GetEvent<PassArtisteEvent>().Subscribe(Update);
            ModifierArtiste = new DelegateCommand<string>(ExecutedA);
            GoToGestionArtiste = new DelegateCommand<string>(ExecutedB);
            Artiste = new Artiste();
            InitialStyles();
            InitialNationalities();

        }

        #region Events
        private void Update(Artiste obj)
        {
            Artiste = obj;
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
