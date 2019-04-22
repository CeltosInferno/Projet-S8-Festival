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


namespace WpfFestival.ViewModels
{
    public class ModifierProgrammationViewModel : BindableBase
    {
        #region Members
        private string _festivalName="name";
        private Festival _festival;
        private Programmation _programmation;
        private List<Programmation> _programmationsList;
        private List<Artiste> _artistesList;
        private List<Scene> _scenesList;
        private bool _isEnabled;
       // private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

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
        public List<Programmation> ProgrammationsList
        {
            get { return _programmationsList; }
            set { SetProperty(ref _programmationsList, value); }
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
        public DelegateCommand ModifierProgrammation { get; private set; }
        private void Executed()
        {

            PutProgrammation($"/api/Programmations/{Programmation.ProgrammationId}");
            //_regionManaager.RequestNavigate("ContentRegion", uri);
        }
        #endregion
        public ModifierProgrammationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Programmation = new Programmation();
            Festival = new Festival();
            
            ModifierProgrammation = new DelegateCommand(Executed).ObservesCanExecute(() => IsEnabled);
            this.ArtistesList = new List<Artiste>();
            this.ScenesList = new List<Scene>();
            this.ProgrammationsList = new List<Programmation>();
            this.GetArtistesList();
            this.GetScenesList();
            this.GetProgrammationsList();
            _eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
        }

        private void PassFestival(Festival obj)
        {
            Festival = obj;
            FestivalName = obj.Name;
            Programmation.FestivalId = obj.Id;
            //Programmation.FestivalId = GetFestival();
            
        }

       
        //public int GetFestival()
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:5575/");
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage response = client.GetAsync($"api/Festivals/{FestivalName}").Result;

        //    if (response.IsSuccessStatusCode)
        //    {

        //        var readTask = response.Content.ReadAsAsync<Festival>();
        //        readTask.Wait();
        //        return readTask.Result.Id;

        //    }
        //    else return 0;


        //}

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

                //HttpResponseMessage result = client.PostAsJsonAsync("/api/festivals", obj).Result;

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

        public void GetProgrammationsList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Programmations").Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<List<Programmation>>();
                readTask.Wait();
                foreach (Programmation p in readTask.Result)
                {
                    this.ProgrammationsList.Add(p);
                }
            }
        }

    }
}
