using Prism.Mvvm;
using Prism.Commands;
using WpfFestival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Prism.Events;
using System.Net.Http;
using System.Net.Http.Headers;
using WpfFestival.Events;
using Prism.Interactivity.InteractionRequest;

namespace WpfFestival.ViewModels
{
    public class ModifierProgrammationViewModel : BindableBase
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
        public int ResultCheck { get; set; }
        public string OriginalName { get; set; }
        
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
        #region Command
        public DelegateCommand<string> ModifierProgrammation { get; private set; }
        public DelegateCommand<string> GoToGestionFestival { get; private set; }
        private void ExecutedA(string uri)
        {   
            if(Programmation.ProgrammationName.Equals(OriginalName))
            {
                ResultCheck = 1;
            }
            else
            {
                ResultCheck = CheckProgrammationName($"api/Programmations/CheckName?name={Programmation.ProgrammationName}");

            }
            if (ResultCheck==1)
            {
                if(PutProgrammation($"/api/Programmations/{Programmation.ProgrammationId}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Modifié !!!", Title = "Notification" });
                    _regionManager.RequestNavigate("ContentRegion", uri);
                    _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste
                }
                else
                {
                    NotificationRequest.Raise(new Notification { Content = "Erreur !!!", Title = "Notification" });

                }

            }
            else if (ResultCheck==0)
            {
                NotificationRequest.Raise(new Notification { Content = "Nom du programme existe, éssayer l'autre nom svp !!!", Title = "Notification" });
            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Erreur serveur !!!", Title = "Notification" });
            }
           
        }
        private void ExecutedB(string uri)
        {
            if (uri != null)
                _regionManager.RequestNavigate("ContentRegion", uri);
        }

        #endregion
        public ModifierProgrammationViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Programmation = new Programmation();
            Festival = new Festival();
            
            ModifierProgrammation = new DelegateCommand<string>(ExecutedA).ObservesCanExecute(() => IsEnabled);
            GoToGestionFestival = new DelegateCommand<string>(ExecutedB);
            this.ArtistesList = new List<Artiste>();
            this.ScenesList = new List<Scene>();
            //this.ProgrammationsList = new List<Programmation>();
            this.GetArtistesList();
            this.GetScenesList();
            NotificationRequest = new InteractionRequest<INotification>();
            //this.GetProgrammationsList();
            //_eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
            _eventAggregator.GetEvent<PassProgrammationEvent>().Subscribe(PassProgrammation);
            
           
        }

       
        private void PassProgrammation (Programmation obj)
        {
            Programmation = obj;
            OriginalName = obj.ProgrammationName;
            this.GetFestivalName($"api/Festivals/{Programmation.FestivalID}");
            Programmation.OrganisateurID = IdentificationViewModel.OrganisateurId;
        }


        public void GetFestivalName(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<Festival>();
                readTask.Wait();
                
                Festival = readTask.Result;

            }
            


        }

        public bool PutProgrammation(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> putProgrammationTask = client.PutAsJsonAsync<Programmation>(uri, Programmation);

                putProgrammationTask.Wait();

                HttpResponseMessage result1 = putProgrammationTask.Result;


                if (result1.IsSuccessStatusCode)
                {
                    return true;

                }
                return false;

            }
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

        public void GetScenesList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Scenes").Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<List<Scene>>();
                readTask.Wait();
                foreach (Scene s in readTask.Result)
                {
                    this.ScenesList.Add(s);
                }
            }
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
    }
}
