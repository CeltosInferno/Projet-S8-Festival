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
using Prism.Events;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class ArtisteFormulaireViewModel : BindableBase
    {
        #region Members
        private Artiste _artiste;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        
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
        public DelegateCommand UploadImage { get; private set; }
        private void ExecutedA(string uri)
        {
            if(PostArtiste("/api/Artistes"))
            {
                NotificationRequest.Raise(new Notification { Content = "Réussi !!!", Title = "Notification" });
                if (uri != null)
                {
                    _regionManager.RequestNavigate("ContentRegion", uri);
                    _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste
                }
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
        //public void ExecutedC() //uploader image
        //{
        //    if(_fileOpen.OpenFile())
        //    {
        //        var selectedFile = this._fileOpen.FileName;
        //    }
        //}
        #endregion
        public ArtisteFormulaireViewModel(IRegionManager regionManager,IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Artiste = new Artiste();
            CreerArtiste = new DelegateCommand<string>(ExecutedA);
            GoToGestionArtiste = new DelegateCommand<string>(ExecutedB);
            //UploadImage = new DelegateCommand(ExecutedC);
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
