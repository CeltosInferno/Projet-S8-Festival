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
using WpfFestival.Views;

namespace WpfFestival.ViewModels
{
    class ProgrammationFormulaireViewModel: BindableBase
    {
        #region Members
        private string _festivalName;
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
        public int ResultCheck { get; set; }
        public string FestivalName
        {
            get { return _festivalName; }
            set { SetProperty(ref _festivalName, value); }
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
        public DelegateCommand<string> GoToGestionFestival { get; private set; }

        private void ExecutedA() // ajouter un programme
        {
            
            ResultCheck = CheckProgrammationName($"api/Programmations/CheckName?name={Programmation.ProgrammationName}");
            if(ResultCheck==1)
            {
                if (PostProgrammation("/api/Programmations"))
                {

                    ConfirmationRequest.Raise(new Confirmation { Title = "Confirmation", Content = "Veuillez créer un autre programme?" }, r => AddNewProgramme = r.Confirmed ? "ProgrammationFormulaire" : "GestionFestival");


                    if (AddNewProgramme.Equals("ProgrammationFormulaire"))
                    {
                        IsEnabled = false;
                        _regionManager.RequestNavigate("ContentRegion", AddNewProgramme);

                    }
                    else if (AddNewProgramme.Equals("GestionFestival"))
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
            else if (ResultCheck==0)
            {
                NotificationRequest.Raise(new Notification { Content = "Nom du programme existe, éssayer l'autre nom svp !!!", Title = "Notification" });

            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Erreur de Serveur !!!", Title = "Notification" });

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
            _programmation.DateFinConcert = DateTime.Now;
            _programmation.DateDebutConcert = DateTime.Now;


            AddProgrammation = new DelegateCommand(ExecutedA).ObservesCanExecute(() => IsEnabled);
            GoToGestionFestival = new DelegateCommand<string>(ExecutedB);
            this.ArtistesList = new List<Artiste>();
            this.ScenesList = new List<Scene>();

            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            NotificationRequest = new InteractionRequest<INotification>();
            ArtistesList = GetArtistesList("api/Artistes");
            ScenesList = GetScenesList("api/Scenes");
            _eventAggregator.GetEvent<PassFestivalNameEvent>().Subscribe(PassFestivalName);


        }

        #region Events
        private void PassFestivalName(string obj)
        {
            FestivalName = obj;
            Programmation.FestivalID = GetFestivalId($"/api/Festivals/FestivalId?name={FestivalName}");
            Programmation.OrganisateurID = IdentificationViewModel.OrganisateurId;

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


                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<Programmation>().Result;
                    return true;
                }
                return false;

            }
        }

        public int GetFestivalId(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<string>(uri, FestivalName);

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
        /* 
        * Vérifier le nom du programme 
        * return -2 erreur de serveur
        * return  0 nom déjà existe
        * return  1 ok!!
        */
        public int CheckProgrammationName(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<string>(uri, Programmation.ProgrammationName);

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
