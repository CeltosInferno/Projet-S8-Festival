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
using Prism.Events;
using WpfFestival.Events;
using Prism.Interactivity.InteractionRequest;

namespace WpfFestival.ViewModels
{
    public class ModifierSceneViewModel : BindableBase
    {
        #region Members
        private Scene _scene;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public Scene Scene
        {
            get { return _scene; }
            set { SetProperty(ref _scene, value); }
        }

        #endregion
        #region Command
        public DelegateCommand<string> ModifierScene { get; private set; }
        public DelegateCommand<string> GoToGestionScene { get; private set; }

        private void ExecutedA(string uri)
        {
            if (PutScene($"api/Scenes/{Scene.Id}"))
            {
                NotificationRequest.Raise(new Notification { Content = "Modifié", Title = "Notification" });
                if (uri != null)
                {
                    _regionManager.RequestNavigate("ContentRegion", uri);
                    _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste

                }
            }
        }
        private void ExecutedB(string uri)
        {
            if (uri != null)
                _regionManager.RequestNavigate("ContentRegion", uri);
        }

        #endregion
        #region Event
        private readonly IEventAggregator _eventAggregator;

        private void PassScene(Scene obj)
        {
            Scene = obj;
        }
        #endregion

        public ModifierSceneViewModel(IRegionManager regionManager,IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PassSceneEvent>().Subscribe(PassScene);
            Scene = new Scene();
            NotificationRequest = new InteractionRequest<INotification>();
            ModifierScene = new DelegateCommand<string>(ExecutedA);
            GoToGestionScene = new DelegateCommand<string>(ExecutedB);
        }

        #region Methods

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
