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
using System.Net.Http.Headers;
using Prism.Interactivity.InteractionRequest;

namespace WpfFestival.ViewModels
{
    public class ArtisteFormulaireViewModel : BindableBase
    {
        #region Members
        private Artiste _artiste;
        private bool _isEnabled;
        #endregion


        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public Artiste Artiste
        {
            get { return _artiste; }
            set { SetProperty(ref _artiste, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand CreerArtiste { get; private set; }
        private void Executed()
        {
            if(PostArtiste("/api/Artistes"))
            {
                NotificationRequest.Raise(new Notification { Content = "Réussi !!!", Title = "Notification" });
            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Réussi ???", Title = "Notification" });
            }
        }
        #endregion
        public ArtisteFormulaireViewModel()
        {
            Artiste = new Artiste();
            CreerArtiste = new DelegateCommand(Executed).ObservesCanExecute(() => IsEnabled);
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
