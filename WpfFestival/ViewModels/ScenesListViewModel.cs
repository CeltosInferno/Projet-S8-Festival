using Prism.Mvvm;
using WpfFestival.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WpfFestival.ViewModels
{
    public class ScenesListViewModel : BindableBase
    {
        //private Scene _scene;
        //private List<Scene> _scenesList;

        //public Scene Scene
        //{
        //    get { return _scene; }
        //    set { SetProperty(ref _scene, value); }
        //}
        //public List<Scene> ScenesList
        //{
        //    get { return _scenesList; }
        //    set { SetProperty(ref _scenesList, value); }
        //}

        //public ScenesListViewModel()
        //{
        //    Scene = new Scene();
        //    ScenesList = new List<Scene>();
        //    this.GetScenesList();
        //}

        //public void GetScenesList()
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:5575/");
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage response = client.GetAsync("api/Scenes").Result;

        //    if (response.IsSuccessStatusCode)
        //    {

        //        var readTask = response.Content.ReadAsAsync<List<Scene>>();
        //        readTask.Wait();
        //        foreach (Scene s in readTask.Result)
        //        {
        //            this.ScenesList.Add(s);
        //        }
        //    }
        //}

        

            
    }

}
