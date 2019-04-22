using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using WpfFestival.Models;
using Prism.Regions;

namespace WpfFestival.ViewModels
{
    public class ModifierSceneViewModel : BindableBase
    {
        #region Members
        private Scene _scene;
        private List<Scene> _scenesList;
        private bool _isEnabled;
#pragma warning disable CS0169 // 从不使用字段“ModifierSceneViewModel._regionManger”
        private readonly IRegionManager _regionManger;
#pragma warning restore CS0169 // 从不使用字段“ModifierSceneViewModel._regionManger”

        #endregion

        #region Properties

        public Scene Scene
        {
            get { return _scene; }
            set { SetProperty(ref _scene, value); }
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
        public DelegateCommand ModifierScene { get; private set; }

        private void Executed()
        {
            if (PutScene($"api/Scenes/{Scene.SceneId}"))
            {

            }
        }
       
        #endregion

        public ModifierSceneViewModel()
        {
            //_regionManger = regionManager;
            //_regionManger.AddToRegion("ScenesList", ScenesList);
            //_regionManger.RequestNavigate("ScenesList", "ScenesList");
            Scene = new Scene();
           
            ModifierScene = new DelegateCommand(Executed).ObservesCanExecute( ()=> IsEnabled);
            this.ScenesList = new List<Scene>();
            this.GetScenesList();
            //GetSceneById();
            //Scene.SceneName = this.GetSceneById().SceneName;
            //Scene.Capacity = this.GetSceneById().Capacity;
        }

        #region Methods
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

        public Scene GetSceneById()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync($"api/Scenes/{Scene.SceneId}").Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<Scene>();
                readTask.Wait();
                return readTask.Result;
               
            }
            return null;
        }

        public bool PutScene(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Task<HttpResponseMessage> response = client.PutAsJsonAsync<Scene>(uri, Scene);
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
