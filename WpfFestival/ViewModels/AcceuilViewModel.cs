﻿using Prism.Events;
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
        private readonly IRegionManager _regionManager;
        
        #region Members
        private ObservableCollection<Festival> _festivalsList;
        private Festival _festival;

        #endregion
        #region Properties
        public InteractionRequest<INotification> NotificationRequest { get; set; }

        public ObservableCollection<Festival> FestivalsList
        {
            get { return _festivalsList; }
            set { SetProperty(ref _festivalsList, value); }
        }
        public Festival Festival //SelectedFestival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value);
            }
        }

        #endregion
        #region Command
        public DelegateCommand<string> GoToModifierFestival { get; private set; }
        public DelegateCommand ModifierFestival { get; private set; }
        public DelegateCommand SupprimerFestival { get; private set; }
        public DelegateCommand RefreshList { get; private set; }

        private void ExecutedA(string uri) //GoToModifierFestival
        {
            if(Festival==null)
            {
                NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" });
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<PassFestivalEvent>().Publish(Festival);
                _eventAggregator.GetEvent<RefreshEvent>().Publish(true);
            }
        }
        private void ExecutedB() //ModifierFestival inscription ou/et publication
        {   
            try
            {
                if (PutFestival($"/api/Festivals/{Festival.Id}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Modifié !!!", Title = "Notification" });
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" }); }
            
            
        }
        private void ExecutedC()
        {
            try
            {
                if (Fonctions.Fonctions.DeleteFestival($"/api/Festivals/{Festival.Id}"))
                {
                    NotificationRequest.Raise(new Notification { Content = "Supprimé !!!", Title = "Notification" });
                    FestivalsList.Remove(Festival);    
                }
            }
            catch (NullReferenceException) { NotificationRequest.Raise(new Notification { Content = "Choisir un festival", Title = "Notification" }); }

        }
        private void ExecutedD()
        {
            FestivalsList = GetFestivalsList();
        }



        #endregion
        public AcceuilViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            GoToModifierFestival = new DelegateCommand<string>(ExecutedA);
            ModifierFestival = new DelegateCommand(ExecutedB);
            SupprimerFestival = new DelegateCommand(ExecutedC);
            RefreshList = new DelegateCommand(ExecutedD);
            NotificationRequest = new InteractionRequest<INotification>();
            
            FestivalsList = new ObservableCollection<Festival>();
            //this.GetFestivalsList();
            FestivalsList = GetFestivalsList();
        }

        #region Methods
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
        
        public ObservableCollection<Festival> GetFestivalsList()
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
                return readTask.Result;
                    
                }
            return null;
            
        }

        //private bool DeleteFestival(string uri)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:5575");
        //        client.DefaultRequestHeaders.Accept.Add(
        //                new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage responseMessage = client.DeleteAsync(uri).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //        return false;

        //    }
        //}
        #endregion

    }
}