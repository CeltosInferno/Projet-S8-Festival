using Prism.Mvvm;
using Prism.Commands;
using Prism.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFestival.Models;
using WpfFestival.ViewModels.Fonctions;
using System.Net.Http;
using Prism.Regions;
using System.Net.Http.Headers;
using Prism.Interactivity.InteractionRequest;

namespace WpfFestival.ViewModels
{
    public class ArtisteFormulaireViewModel : BindableBase
    {
        #region Members
        private Artiste _artiste;
        private readonly IRegionManager _regionManager;
        #endregion


        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public Artiste Artiste
        {
            get { return _artiste; }
            set { SetProperty(ref _artiste, value); }
        }
       
        #endregion

        #region Commands
        public DelegateCommand<string> CreerArtiste { get; private set; }
        public DelegateCommand<string> GoToGestionArtiste { get; private set; }
        private void ExecutedA(string uri)
        {
            if(PostArtiste("/api/Artistes"))
            {
                NotificationRequest.Raise(new Notification { Content = "Réussi !!!", Title = "Notification" });
                if (uri != null)
                    _regionManager.RequestNavigate("ContentRegion", uri);
                    
            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Réussi ???", Title = "Notification" });
            }
        }
        private void ExecutedB(string uri)
        {
            if (uri != null)
                _regionManager.RequestNavigate("ContentRegion", uri);
        }
        #endregion
        public ArtisteFormulaireViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Artiste = new Artiste();
            CreerArtiste = new DelegateCommand<string>(ExecutedA);
            GoToGestionArtiste = new DelegateCommand<string>(ExecutedB);
            NotificationRequest = new InteractionRequest<INotification>();
            
        }

        #region Methods
        private bool PostArtiste(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postFestivalTask = client.PostAsJsonAsync<Artiste>(uri, Artiste);

                postFestivalTask.Wait();

                HttpResponseMessage result1 = postFestivalTask.Result;

                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<Artiste>().Result;
                    return true;
                }
                return false;

            }
        }
        #endregion

    }
}
