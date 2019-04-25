using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfFestival.Models;
using Prism.Commands;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class AcceuilViewModel :BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManaager;
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        
        #region Members
        private ObservableCollection<Festival> _festivalsList;
        private Festival _festival;
        private bool _isEnabled;
        #endregion
        #region Properties
        public ObservableCollection<Festival> FestivalsList
        {
            get { return _festivalsList; }
            set { SetProperty(ref _festivalsList, value); }
        }
        public Festival Festival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value, onChanged);
            }
        }

        public bool IsPublication
        {
            get { return Festival.IsPublication; }
            set
            { Festival.IsPublication = value;
                RaisePropertyChanged();
            }
        }
        private void onChanged()
        { PutFestival($"/api/{Festival.Id}"); }
        private  void RaisePropertyChanged()
        {
            PutFestival($"/api/{Festival.Id}");
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        #endregion
        #region Command
        public DelegateCommand<string> GoToModifierFestival { get; private set; }
        public DelegateCommand ModifierFestival { get; private set; }

        private void Executed(string uri)
        {
            if(Festival==null)
            {
                NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" });
            }
            else
            {
                _regionManaager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassFestivalEvent>().Publish(Festival);
            }
        }
        private void ExecutedB()
        {   
            try
            {
                if (PutFestival($"/api/Festivals/{Festival.Id}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Modifié !!!", Title = "Notification" });
                }
                else
                {
                    NotificationRequest.Raise(new Notification { Content = "Notification Message", Title = "Notification" });
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" }); }
            
            
        }

        #endregion
        public AcceuilViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManaager = regionManager;
            _eventAggregator = eventAggregator;
            GoToModifierFestival = new DelegateCommand<string>(Executed);
            ModifierFestival = new DelegateCommand(ExecutedB);
            NotificationRequest = new InteractionRequest<INotification>();
            
            FestivalsList = new ObservableCollection<Festival>();
            this.GetFestivalsList();
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

                HttpResponseMessage result1 = putFestivalTask.Result;

                if (result1.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;

            }
        }
        public void GetFestivalsList()
        {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5575/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Festivals").Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsAsync<ObservableCollection<Festival>>();
                    readTask.Wait();
                    foreach (Festival f in readTask.Result)
                    {
                        this.FestivalsList.Add(f);
                    }
                }
            
        }
        
    }
}
