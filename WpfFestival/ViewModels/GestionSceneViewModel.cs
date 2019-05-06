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
    public class GestionSceneViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        #region Members
        private Scene _scene;
        private ObservableCollection<Scene> _scenesList;
        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public Scene Scene // SelectedScene
        {
            get { return _scene; }
            set { SetProperty(ref _scene, value); }
        }
        public ObservableCollection<Scene> ScenesList
        {
            get { return _scenesList; }
            set { SetProperty(ref _scenesList, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> CreerScene { get; private set; }
        public DelegateCommand<string> ModifierScene { get; private set; }
        public DelegateCommand SupprimerScene { get; private set; }

        private void ExecutedA (string uri) //CreerScene
        {   
            //if(uri != null)
             _regionManager.RequestNavigate("ContentRegion", uri);
        }
        private void ExecutedB(string uri) // ModiferScene
        {
            if (Scene == null)
            {
                NotificationRequest.Raise(new Notification { Content = "Choisir un scène", Title = "Notification" });
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassSceneEvent>().Publish(Scene);
            }
        }
        private void ExecutedC () //SupprimerScene
        {
            try
            {
                if (Fonctions.Fonctions.DeleteScene($"/api/Scenes/{Scene.Id}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Supprimé !!!", Title = "Notification" });
                    ScenesList.Remove(Scene);
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un Scene", Title = "Notification" }); }

        }
        #endregion
        public GestionSceneViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            CreerScene = new DelegateCommand<string>(ExecutedA);
            ModifierScene = new DelegateCommand<string>(ExecutedB);
            SupprimerScene = new DelegateCommand(ExecutedC);
            NotificationRequest = new InteractionRequest<INotification>();
            ScenesList = new ObservableCollection<Scene>();

            _eventAggregator.GetEvent<RefreshEvent>().Subscribe(Update);
            //this.GetScenesList("/api/Scenes");
        }
        #region Events
        private void Update(bool obj)
        {
            if(obj)
            {
                ScenesList = GetScenesList("/api/Scenes");
                obj = false;
            }
        }
        #endregion
        #region Methods
        private void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" });
        }

        public ObservableCollection<Scene> GetScenesList(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var readTask = response.Content.ReadAsAsync<ObservableCollection<Scene>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }
        #endregion
    }
}
