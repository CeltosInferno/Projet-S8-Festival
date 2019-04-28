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

namespace WpfFestival.ViewModels
{
    public class SceneFormulaireViewModel : BindableBase
    {
        #region Members
        private Scene _scene;
        private readonly IRegionManager _regionManger;
        #endregion
        #region Properties
       

        public Scene Scene
        {
            get { return _scene; }
            set { SetProperty(ref _scene, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand CreerScene { get; private set; }
        public DelegateCommand<string> GoToAcceuil { get; private set; }
        private void ExecutedA() // Créer Scene
        {
           if( PostScene("/api/Scenes"))
            {
                NotificationRequest.Raise(new Notification { Content = "Créé !!!", Title = "Notification" });
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
        #region Interaction
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        private void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" });
        }
        #endregion

        #region Constructor
        public SceneFormulaireViewModel(IRegionManager regionManager)
        {
            _regionManger = regionManager;
            NotificationRequest = new InteractionRequest<INotification>();
            CreerScene = new DelegateCommand(ExecutedA);
            GoToAcceuil = new DelegateCommand<string>(ExecutedB);

            Scene = new Scene();
            Scene.SceneName = "Nouveau nom";
               

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
