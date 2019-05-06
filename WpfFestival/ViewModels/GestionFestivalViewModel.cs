using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfFestival.Models;
using Prism.Commands;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class GestionFestivalViewModel :BindableBase
    {
        
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        #region Members
        private ObservableCollection<Festival> _festivalsList;
        private Festival _festival;

        #endregion
        #region Properties
        public bool IsEnabled { get; set; }
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public ObservableCollection<Festival> FestivalsList
        {
            get { return _festivalsList; }
            set { SetProperty(ref _festivalsList, value); }
        }
        public Festival Festival //SelectedFestival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value); }
        }

        #endregion
        #region Commands
        public DelegateCommand<string> GoToModifierFestival { get; private set; }
        public DelegateCommand<string> GoToFestivalFormulaire { get; private set; }
        public DelegateCommand ModifierFestival { get; private set; } // modifier pulication et inscription
        public DelegateCommand SupprimerFestival { get; private set; }

        private void ExecutedA(string uri) //GoToModifierFestival
        {
            if (Festival == null)
            {
                NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" });
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassFestivalEvent>().Publish(Festival);
                _eventAggregator.GetEvent<RefreshEvent>().Publish(true);
            }
        }
        private void ExecutedB() //ModifierFestival inscription ou/et publication
        {
            try
            {
                if (PutFestival($"/api/Festivals/{Festival.Id}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Modifié !!!", Title = "Notification" });
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" }); }


        }
        private void ExecutedC() // supprimer le festival
        {
            try
            {
                if (Fonctions.Fonctions.DeleteFestival($"/api/Festivals/{Festival.Id}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Supprimé !!!", Title = "Notification" });
                    FestivalsList.Remove(Festival);
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" }); }

        }
        private void ExecutedD(string uri) //GoTo Création page
        {
            if (uri != null)
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
                //_eventAggregator.GetEvent<PassOrganisateurIdEvent>().Publish(Festival.OrganisateurId);
            }

        }
        #endregion
        public GestionFestivalViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            Festival = new Festival();
            GoToModifierFestival = new DelegateCommand<string>(ExecutedA);
            ModifierFestival = new DelegateCommand(ExecutedB);
            SupprimerFestival = new DelegateCommand(ExecutedC);
            GoToFestivalFormulaire = new DelegateCommand<string>(ExecutedD);
            NotificationRequest = new InteractionRequest<INotification>();

            FestivalsList = new ObservableCollection<Festival>();

            //_eventAggregator.GetEvent<PassOrganisateurIdEvent>().Subscribe(GetOrganisateurId);
            _eventAggregator.GetEvent<RefreshEvent>().Subscribe(Update);
           
            
        }
        
        #region Events
        private void GetOrganisateurId(int obj)
        {
            
            FestivalsList = GetFestivalsList($"api/Festivals/org/{obj}");

        }
        private void Update(bool obj)
        {
            if (obj)
            {
                
                FestivalsList = GetFestivalsList($"api/Festivals/org/{IdentificationViewModel.OrganisateurId}");
                obj = false;
            }
        }
        #endregion
       
        #region Methods


        private bool PutFestival(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> putFestivalTask = client.PutAsJsonAsync<Festival>(uri, Festival);

                putFestivalTask.Wait();

                HttpResponseMessage result1 = putFestivalTask.Result;

                if (result1.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;

            }
        }
        
        public ObservableCollection<Festival> GetFestivalsList(string uri)
        {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5575/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsAsync<ObservableCollection<Festival>>();
                    readTask.Wait();
                return readTask.Result;
                    
                }
            return null;
            
        }
        
        #endregion

    }
}
