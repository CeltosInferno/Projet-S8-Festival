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

namespace WpfFestival.ViewModels
{
    class ProgrammationFormulaireViewModel: BindableBase
    {
        
        #region Members
        private string _festivalName ;
        private Festival _festival;
        private Programmation _programmation;
        private List<Artiste> _artistesList;
        private List<Scene> _scenesList;
        private bool _isEnabled;
#pragma warning disable CS0169 // 从不使用字段“ProgrammationFormulaireViewModel._regionManager”
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
#pragma warning restore CS0169 // 从不使用字段“ProgrammationFormulaireViewModel._regionManager”

        #endregion

        #region Properties
        public string FestivalName
        {
            get { return _festivalName; }
            set { SetProperty(ref _festivalName, value); }
        }
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
        public DelegateCommand AddProgrammation { get; private set; }

        public ProgrammationFormulaireViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Programmation = new Programmation();
            //Festival = new Festival();
            _eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
            
            AddProgrammation = new DelegateCommand(Executed).ObservesCanExecute(() => IsEnabled);
            this.ArtistesList = new List<Artiste>();
            this.ScenesList = new List<Scene>();
            
            this.GetArtistesList();
            this.GetScenesList();
        }

        private void PassFestival(Festival obj)
        {
            Festival = obj;
            //Programmation.FestivalId = GetFestivalId();
            Programmation.FestivalId = Festival.Id;
            Console.WriteLine(Programmation.FestivalId);
            Console.WriteLine(Festival.Id);
        }

        private void Executed() {
            
            PostProgrammation();
            //_regionManaager.RequestNavigate("ContentRegion", uri);
        }
        public int GetFestivalId()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"api/Festivals/{FestivalName}").Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<Festival>();
                readTask.Wait();
                return readTask.Result.Id;

            }
            else return 0;
            
            
        }

        public void PostProgrammation()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postProgrammationTask = client.PostAsJsonAsync<Programmation>("/api/Programmations", Programmation);

                postProgrammationTask.Wait();

                HttpResponseMessage result1 = postProgrammationTask.Result;

                //HttpResponseMessage result = client.PostAsJsonAsync("/api/festivals", obj).Result;

                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<Programmation>().Result;

                }

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
    }
}
