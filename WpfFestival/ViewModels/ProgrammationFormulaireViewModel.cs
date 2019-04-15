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
        private string _festivalName = "name";
        private Programmation _programmation;
        //private Artiste _artiste;
        //private Scene _scene;
        private List<Artiste> _artistesList;
        private int _selectedArtiste;
        private List<Scene> _scenesList;
        private int _selectedScene;
        private bool _isEnabled;
        private readonly IRegionManager _regionManaager;

        #endregion
        //public Festival f { get; set; }
        //public List<string> ProgrammationsList { get; set; }
        #region Properties
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
        public int SelectedArtiste
        {
            get { return _selectedArtiste; }
            set { SetProperty(ref _selectedArtiste, value); }
        }
        public List<Scene> ScenesList
        {
            get { return _scenesList; }
            set { SetProperty(ref _scenesList, value); }
        }
        public int SelectedScene
        {
            get { return _selectedScene; }
            set { SetProperty(ref _selectedScene, value); }
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
            Programmation = new Programmation();
            
            eventAggregator.GetEvent<PassFestivalNameEvent>().Subscribe(Updated);
            AddProgrammation = new DelegateCommand(Executed).ObservesCanExecute(() => IsEnabled);
            this.ArtistesList = new List<Artiste>();
            this.ScenesList = new List<Scene>();
            
            this.GetArtistesList();
            this.GetScenesList();
        }

        private void Updated(string obj)
        {
            FestivalName = obj;
            Programmation.FestivalId = GetFestival();
        }

        private void Executed() {
            
            PostProgrammation();
            //_regionManaager.RequestNavigate("ContentRegion", uri);
        }
        //public async Task<Festival> GetFestivalAsync()
        //{

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:5575/");
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage response = await client.GetAsync("api/Festivals/name");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var readTask = await response.Content.ReadAsAsync<Festival>();

        //        return readTask;
        //    }
        //    else { return null; }
        //}

        public int GetFestival()
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
