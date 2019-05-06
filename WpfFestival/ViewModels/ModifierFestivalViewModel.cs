using Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFestival.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using WpfFestival.Events;
using System.Collections.ObjectModel;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace WpfFestival.ViewModels
{
    public class ModifierFestivalViewModel : BindableBase
    {
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        #region Members
        private Festival _festival;
        private Programmation _programmation;
        private ObservableCollection<Programmation> _programmationsList;
        
        private bool _isEnabled;
        #endregion
        
        #region Properties
        public int ResultCheck { get; set; }
        public string OriginalName { get; set; }
        public Festival Festival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value); }
        }
        public Programmation Programmation // SelectedProgrammation
        {
            get { return _programmation; }
            set { SetProperty(ref _programmation, value); }
        }
        public ObservableCollection<Programmation> ProgrammationsList
        {
            get { return _programmationsList; }
            set { SetProperty(ref _programmationsList, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        #endregion

        #region Command
        public DelegateCommand ModifierFestival { get; private set; }
        public DelegateCommand<string> GoToModifierProgrammation { get; private set; }
        public DelegateCommand SupprimerProgrammation { get; private set; }
        public DelegateCommand<string> GoToProgrammationFormulaire { get; private set; }
        public DelegateCommand RefreshList { get; private set; }

        private void ExecutedA() // ModifierFestival
        {   
            if(Festival.Nom.Equals(OriginalName))
            {
                ResultCheck = 1;
            }
            else
            {
                ResultCheck = CheckFestivalName($"/api/Festivals/CheckName?name={Festival.Nom}");
            }
            if(ResultCheck==1) // vérifier nom
            {
                if(Festival.DateFin.CompareTo(Festival.DateDebut)<0) //vérifier date
                {
                    NotificationRequest.Raise(new Notification { Content = "Check date !!", Title = "Notification" });
                }
                else
                {
                    if (PutFestival($"/api/Festivals/{Festival.Id}"))
                    {
                        NotificationRequest.Raise(new Notification { Content = "Modifié", Title = "Notification" });
                    }
                    else
                    {
                        NotificationRequest.Raise(new Notification { Content = "Erreur de motification", Title = "Notification" });
                    }
                }
            }
            else if(ResultCheck==0)
            {
                NotificationRequest.Raise(new Notification { Content = "Nom déjà existe, essayer l'autre nom svp", Title = "Notification" });
            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Erreur de serveur", Title = "Notification" });
            }
        }
        private void ExecutedB(string uri) // GoToModifierProgrammation
        {
            if (Programmation == null)
            {
                NotificationRequest.Raise(new Notification { Content = "Choisir un programme", Title = "Notification" });
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassProgrammationEvent>().Publish(Programmation);
            }
        }
        private void ExecutedC() // SupprimerProgrammation
        {

            try
            {
                if (Fonctions.Fonctions.DeleteProgrammation($"/api/Programmations/{Programmation.ProgrammationId}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Supprimé !!!", Title = "Notification" });
                    ProgrammationsList.Remove(Programmation);
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un programme", Title = "Notification" }); }

        }
        private void ExecutedD(string uri) // GoToProgrammationFormulaire pour créer un programme
        {
            if(uri != null)
            {   
                
                
                _regionManager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassFestivalNameEvent>().Publish(Festival.Nom);
            }
              
        }
        private void ExecutedE() //refresh list
        {
            ProgrammationsList = GetProgrammationsList($"api/Programmations/{Festival.Id}");
        }

        #endregion

        public ModifierFestivalViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Festival = new Festival();
            ModifierFestival = new DelegateCommand(ExecutedA).ObservesCanExecute(()=>IsEnabled);
            GoToModifierProgrammation = new DelegateCommand<string>(ExecutedB);
            SupprimerProgrammation = new DelegateCommand(ExecutedC);
            GoToProgrammationFormulaire = new DelegateCommand<string>(ExecutedD);
            RefreshList = new DelegateCommand(ExecutedE);
            _eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
            _eventAggregator.GetEvent<RefreshEvent>().Subscribe(Update);
            NotificationRequest = new InteractionRequest<INotification>();

            
            ProgrammationsList = new ObservableCollection<Programmation>();
        }
        #region Events
        private void PassFestival(Festival obj)
        {
            Festival = obj;
            OriginalName = obj.Nom;
            
        }
        private void Update(bool obj)
        {
            if(obj)
            {
                ProgrammationsList = GetProgrammationsList($"api/Programmations/{Festival.Id}");
                obj = false;
            }
        }

        #endregion
        #region Methods
        private bool PutFestival(string uri)
        {
            using (var client = new HttpClient())
            {   
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> putFestivalTask = client.PutAsJsonAsync<Festival>(uri, Festival);

                putFestivalTask.Wait();

                HttpResponseMessage result = putFestivalTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;

                }
                return false;

            }
        }

        private ObservableCollection<Programmation> GetProgrammationsList(string uri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<ObservableCollection<Programmation>>();
                readTask.Wait();
                return readTask.Result;
            }
            return null;
        }

        /* 
       * Vérifier le nom du festival 
       * return -2 erreur de serveur
       * return  0 nom déjà existe
       * return  1 ok!!
       */
        public int CheckFestivalName(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<string>(uri, Festival.Nom);

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
        #endregion
    }
}
