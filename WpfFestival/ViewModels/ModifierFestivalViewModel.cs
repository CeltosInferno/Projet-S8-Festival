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
        
        private bool _isEnabled;
        #endregion
        
        #region Properties
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

        private void ExecutedA()
        {
            if(PutFestival($"/api/Festivals/{Festival.Id}"))
            {
                NotificationRequest.Raise(new Notification { Content = "Modifié", Title = "Notification" });
            }
            else
            {
                NotificationRequest.Raise(new Notification { Content = "Modifié§§§", Title = "Notification" });
            }
        }
        private void ExecutedB(string uri)
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
        private void ExecutedC()
        {

            try
            {
                if (Fonctions.Fonctions.DeleteProgrammation($"/api/Programmations/{Programmation.ProgrammationId}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Supprimé !!!", Title = "Notification" });
                    Festival.ProgrammationsList.Remove(Programmation);
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un programme", Title = "Notification" }); }

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
            _eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
            NotificationRequest = new InteractionRequest<INotification>();
            //Festival.ProgrammationsList = new ObservableCollection<Programmation>();
            //this.GetProgrammationsList();
        }

        private void PassFestival(Festival obj)
        {
            Festival = obj;
        }
        private void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" });
        }

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

        private void GetProgrammationsList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5575/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Programmations").Result;

            if (response.IsSuccessStatusCode)
            {

                var readTask = response.Content.ReadAsAsync<List<Programmation>>();
                readTask.Wait();
                foreach (Programmation p in readTask.Result)
                {
                    this.Festival.ProgrammationsList.Add(p);
                }
            }
        }
       
    }
}
