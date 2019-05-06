using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfFestival.Models;
using WpfFestival.Events;
using Prism.Interactivity.InteractionRequest;

namespace WpfFestival.ViewModels
{
    public class FestivalFormulaireViewModel : BindableBase
    {
        #region Members
        private Festival _festival = new Festival();
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManaager;
        private bool _isEnabled;
        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public int ResultCheck { get; set; }
        public Festival Festival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value);
                //AddFestival.RaiseCanExecuteChanged();
            }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> AddFestival { get; private set; }
        public DelegateCommand<string> GoToGestionFestival { get; private set; }
        private void ExecutedA(string uri)
        {
            ResultCheck = CheckFestivalName($"/api/Festivals/CheckName?name={Festival.Nom}");
            if(ResultCheck==1)
            {
                if(Festival.DateFin.CompareTo(Festival.DateDebut)<0)
                {
                    NotificationRequest.Raise(new Notification { Content = "Erreur de Date , éssayer l'autre date svp !!!", Title = "Notification" });

                }
                else
                {
                    if (PostFestival("/api/Festivals"))
                    {
                        NotificationRequest.Raise(new Notification { Content = "Festival est créé, continuer à créer la programmation !!!", Title = "Notification" });

                        _regionManaager.RequestNavigate("ContentRegion", uri);
                        _eventAggregator.GetEvent<PassFestivalNameEvent>().Publish(Festival.Nom);
                    }
                    else
                    {
                        NotificationRequest.Raise(new Notification { Content = "Erreur !!!", Title = "Notification" });

                    }
                }

                
            }
            else if(ResultCheck==0)
            {
                NotificationRequest.Raise(new Notification { Content = "Nom de festival existe, éssayer l'autre nom svp !!!", Title = "Notification" });
            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Erreur de serveur !!!", Title = "Notification" });
            }

        }
        private void ExecutedB(string uri)
        {
            if (uri != null)
                _regionManaager.RequestNavigate("ContentRegion", uri);
        }
        #endregion
        
        
        public FestivalFormulaireViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManaager = regionManager;
            AddFestival = new DelegateCommand<string>(ExecutedA).ObservesCanExecute(() => IsEnabled);
            GoToGestionFestival = new DelegateCommand<string>(ExecutedB);
            //_eventAggregator.GetEvent<PassOrganisateurIdEvent>().Subscribe(Update);
            _festival.DateFin = DateTime.Now;
            _festival.DateDebut = DateTime.Now;

            NotificationRequest = new InteractionRequest<INotification>();
            Festival.OrganisateurId = IdentificationViewModel.OrganisateurId;
        }

        #region Events
        private void Update(int obj)
        {
            Festival.OrganisateurId = obj;
        }
        #endregion

        #region Methods
        private bool PostFestival(string uri)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postFestivalTask = client.PostAsJsonAsync<Festival>(uri, Festival);

                postFestivalTask.Wait();

                HttpResponseMessage result = postFestivalTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Festival>().Result;
                    return true;
                }
                return false;

            }

        }
        /* 
        * Vérifier le nom du festival 
        * return -2 erreur de serveur
        * return  0 nom déjà existe
        * return  1 ok!!
        */
        public int CheckFestivalName(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<string>(uri, Festival.Nom);

                postTask.Wait();

                HttpResponseMessage result1 = postTask.Result;

                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<int>().Result;
                    return readTask;
                }
                return -2;

            }
        }

        #endregion





    }
}
