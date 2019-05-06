using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFestival.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Prism.Interactivity.InteractionRequest;
using Prism.Events;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class SceneFormulaireViewModel : BindableBase
    {
        #region Members
        private Scene _scene;
        private readonly IRegionManager _regionManger;
        private readonly IEventAggregator _eventAggregator;
        #endregion
        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }

        public Scene Scene
        {
            get { return _scene; }
            set { SetProperty(ref _scene, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand<string> CreerScene { get; private set; }
        public DelegateCommand<string> GoToGestionScene { get; private set; }
        private void ExecutedA(string uri) // Créer Scene
        {
           if( PostScene("/api/Scenes"))
            {
                NotificationRequest.Raise(new Notification { Content = "Créé !!!", Title = "Notification" });
                if (uri != null)
                {
                    _regionManger.RequestNavigate("ContentRegion", uri);

                    _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste

                }
            }
           else
                NotificationRequest.Raise(new Notification { Content = "Créé ??", Title = "Notification" });
        }
        private void ExecutedB(string uri)
        {
            if(uri != null)
                _regionManger.RequestNavigate("ContentRegion", uri);
        }
        #endregion
        

        #region Constructor
        public SceneFormulaireViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManger = regionManager;
            _eventAggregator = eventAggregator;
            NotificationRequest = new InteractionRequest<INotification>();
            CreerScene = new DelegateCommand<string>(ExecutedA);
            GoToGestionScene = new DelegateCommand<string>(ExecutedB);

            Scene = new Scene();
            Scene.Nom = "Entrer un nom";
               

        }


        #endregion
        #region Methods
        public bool PostScene(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postSceneTask = client.PostAsJsonAsync<Scene>(uri, Scene);

                postSceneTask.Wait();

                HttpResponseMessage result = postSceneTask.Result;


                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Scene>().Result;
                    return true;
                }
                return false;

            }
        }
        #endregion
    }
}
