using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WpfFestival.Models;
using System.ComponentModel;
using Prism.Mvvm;
using Prism.Events;
using Prism.Commands;
using Prism.Regions;
using WpfFestival.Events;
using Prism.Interactivity.InteractionRequest;

namespace WpfFestival.ViewModels
{
    class ProgrammationFormulaireViewModel: BindableBase
    {
        
        #region Members
        private Festival _festival;
        private Programmation _programmation;
        private List<Artiste> _artistesList;
        private List<Scene> _scenesList;
        private bool _isEnabled;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }
        public string AddNewProgramme { get; set; }
        public Festival Festival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value); }
        }
        public Programmation Programmation
        {
            get { return _programmation; }
            set { SetProperty(ref _programmation, value); }
        }
       
       
        public List<Artiste> ArtistesList
        {
            get { return _artistesList; }
            set { SetProperty(ref _artistesList, value); }
            
        }
        
        public List<Scene> ScenesList
        {
            get { return _scenesList; }
            set { SetProperty(ref _scenesList, value); }
        }
       
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        

        #endregion
        #region Commands
        public DelegateCommand AddProgrammation { get; private set; }
        public DelegateCommand<string> GoToAcceuil { get; private set; }

        private void ExecutedA() // ajouter un programme
        {

            if(PostProgrammation("/api/Programmations"))
            {

                ConfirmationRequest.Raise(new Confirmation { Title = "Confirmation", Content = "Veuillez créer un autre programme?" }, r => AddNewProgramme = r.Confirmed ?  "ProgrammationFormulaire" : "Acceuil" );
               

                if (AddNewProgramme.Equals("ProgrammationFormulaire"))
                {
                    //_regionManager.RequestNavigate("ContentRegion", Confirmed);
                    //_programmation.ProgrammationName = "1";
                    //IsEnabled = false;
                    //Programmation.ArtisteId = 0;
                    _regionManager.RequestNavigate("ContentRegion", AddNewProgramme);

                }
                else if (AddNewProgramme.Equals("Acceuil"))
                {
                    _regionManager.RequestNavigate("ContentRegion", AddNewProgramme);
                    _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste
                }

            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Erreur", Title = "Notification" });

            }


        }

        private void ExecutedB(string uri) // Retour à l'acceuil
        {
            if (uri != null)
                _regionManager.RequestNavigate("ContentRegion", uri);
        }
        #endregion


        public ProgrammationFormulaireViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Programmation = new Programmation();
            //Festival = new Festival();
            _eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
            

            AddProgrammation = new DelegateCommand(ExecutedA).ObservesCanExecute(() => IsEnabled);
            GoToAcceuil = new DelegateCommand<string>(ExecutedB);
            this.ArtistesList = new List<Artiste>();
            this.ScenesList = new List<Scene>();

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            NotificationRequest = new InteractionRequest<INotification>();
            ArtistesList = GetArtistesList("api/Artistes");
            ScenesList = GetScenesList("api/Scenes");

        }
       
        #region Events
        private void PassFestival(Festival obj)
        {
            Festival = obj;
            Programmation.FestivalId = Festival.Id;
           
        }
        #endregion

        #region Methods
        
        public bool PostProgrammation(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postProgrammationTask = client.PostAsJsonAsync<Programmation>(uri, Programmation);

                postProgrammationTask.Wait();

                HttpResponseMessage result1 = postProgrammationTask.Result;

                //HttpResponseMessage result = client.PostAsJsonAsync("/api/festivals", obj).Result;

                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<Programmation>().Result;
                    return true;
                }
                return false;

            }
        }

        public List<Artiste> GetArtistesList(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<List<Artiste>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        public List<Scene> GetScenesList(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<List<Scene>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }
        #endregion
    }
}
