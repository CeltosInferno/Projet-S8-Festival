using Prism.Mvvm;
using Prism.Events;
using Prism.Regions;
using Prism.Commands;
using WpfFestival.Models;
using WpfFestival.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Prism.Interactivity.InteractionRequest;
using System.Security.Cryptography;
using System.Text;
using WpfFestival.Views;

namespace WpfFestival.ViewModels
{
    public class IdentificationViewModel : BindableBase
    {
        #region Members
        public static int OrganisateurId;
        private Organisateur _organisateur;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        #endregion

        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }

        public Organisateur Organisateur
        {
            get { return _organisateur; }
            set { SetProperty(ref _organisateur, value); }
        }
        public int ResultCheckPassword { get; set; }
        #endregion
        #region Commands
        public DelegateCommand<string> Verifier { get; private set; }

        private void Executed(string uri)
        {
            try
            {
                ResultCheckPassword = CheckPassword($"/api/Organisateurs");
               
                if (ResultCheckPassword == -1)
                {
                    NotificationRequest.Raise(new Notification { Content = "Erreur Mot de passe !!!", Title = "Notification" });
                }
                else if (ResultCheckPassword == 0)
                {
                    NotificationRequest.Raise(new Notification { Content = "Email n'existe pas !!!", Title = "Notification" });
                }
                else if (ResultCheckPassword == 1)
                {
                    Organisateur.Id = GetOrganisateurId($"/api/Organisateurs/{Organisateur.Login}").Id;
                    OrganisateurId = Organisateur.Id;
                    NotificationRequest.Raise(new Notification { Content = "Bienvenue !!!", Title = "Notification" });
                    _regionManager.RequestNavigate("MainContentRegion", uri);
                    //_eventAggregator.GetEvent<PassOrganisateurIdEvent>().Publish(Organisateur.Id);
                    
                }
                else if (ResultCheckPassword == -2)
                {
                    NotificationRequest.Raise(new Notification { Content = "Server Error !!!", Title = "Notification" });

                }
            }
            catch(NullReferenceException)
            {
                NotificationRequest.Raise(new Notification { Content = "Entrez email ou mot de pass  !!!", Title = "Notification" });
            }


        }
        #endregion


        public IdentificationViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager; 
            Verifier = new DelegateCommand<string>(Executed);
            Organisateur = new Organisateur();
            NotificationRequest = new InteractionRequest<INotification>();
        }
        #region Methods
        /* 
         * Vérifier le mot de passe 
         * return -2 erreur de serveur
         * return -1 mot de passe n'est pas bon
         * return  0 email n'existe pas
         * return  1 ok!!
         */
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public int CheckPassword(string uri)
        {
            MD5 md5hash = MD5.Create();
            string mdpcrypte = GetMd5Hash(md5hash, Identification.password);
            Organisateur.Mdp = mdpcrypte;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<Organisateur>(uri, Organisateur);

                postTask.Wait();

                HttpResponseMessage result1 = postTask.Result;

                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsAsync<int>().Result;
                    return readTask;
                }
                return -2;

            }
        }
        public Organisateur GetOrganisateurId(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsAsync<Organisateur>();
                    readTask.Wait();
                    return readTask.Result;

                }
                return null; //server error

            }
        }
        #endregion
    }
}
