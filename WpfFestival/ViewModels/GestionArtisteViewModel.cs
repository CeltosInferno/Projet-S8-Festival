using Prism.Events;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using WpfFestival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Prism.Interactivity.InteractionRequest;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class GestionArtisteViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        #region Members
        private Artiste _artiste;
        private ObservableCollection<Artiste> _artistesList;
        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public Artiste Artiste // SelectedArtiste
        {
            get { return _artiste; }
            set { SetProperty(ref _artiste, value); }
        }
        public ObservableCollection<Artiste> ArtistesList
        {
            get { return _artistesList; }
            set { SetProperty(ref _artistesList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> CreerArtiste { get; private set; }
        public DelegateCommand<string> ModifierArtiste { get; private set; }
        public DelegateCommand SupprimerArtiste { get; private set; }

        private void ExecutedA(string uri) //CreerArtiste
        {
            if (uri != null)
                _regionManager.RequestNavigate("ContentRegion", uri);
        }
        private void ExecutedB(string uri) // ModiferArtiste
        {
            if (Artiste == null)
            {
                NotificationRequest.Raise(new Notification { Content = "Choisir un artiste", Title = "Notification" });
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassArtisteEvent>().Publish(Artiste);
            }
        }
        private void ExecutedC() //SupprimerArtiste
        {
            try
            {
                if (Fonctions.Fonctions.DeleteScene($"/api/Artistes/{Artiste.ArtisteID}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Supprimé !!!", Title = "Notification" });
                    ArtistesList.Remove(Artiste);
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un artiste", Title = "Notification" }); }

        }
        #endregion
        public GestionArtisteViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            CreerArtiste = new DelegateCommand<string>(ExecutedA);
            ModifierArtiste = new DelegateCommand<string>(ExecutedB);
            SupprimerArtiste = new DelegateCommand(ExecutedC);
            NotificationRequest = new InteractionRequest<INotification>();
            ArtistesList = new ObservableCollection<Artiste>();

            _eventAggregator.GetEvent<RefreshEvent>().Subscribe(Update);
            //ArtistesList = GetArtistesList("/api/Artistes");


        }
        #region Events
        private void Update(bool obj)
        {
            if(obj)
            {
                ArtistesList = GetArtistesList("/api/Artistes");
                obj = false;
            }
        }
        #endregion
        #region Methods
        private void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" });
        }

        public ObservableCollection<Artiste> GetArtistesList(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var readTask = response.Content.ReadAsAsync<ObservableCollection<Artiste>>();
                readTask.Wait();
                //foreach (Artiste s in readTask.Result)
                //{
                //    this.ArtistesList.Add(s);
                //}
                return readTask.Result;
            }
            return null;
        }
        #endregion
    }
}

