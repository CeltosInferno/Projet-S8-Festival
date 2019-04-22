using Prism.Mvvm;
using Prism;
using Prism.Commands;
using System.Collections.Generic;
using WpfFestival.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace WpfFestival.ViewModels
{
    public class ProgrammationsListViewModel : BindableBase
    {
        private Programmation _selectedProgrammation;
        private List<Programmation> _programmationsList;

        public Programmation SelectedProgrammation
        {
            get { return _selectedProgrammation; }
            set { SetProperty(ref _selectedProgrammation, value); }
        }
        public List<Programmation> ProgrammationsList
        {
            get { return _programmationsList; }
            set { SetProperty(ref _programmationsList, value); }
        }

        public ProgrammationsListViewModel()
        {
            SelectedProgrammation = new Programmation();
            ProgrammationsList = new List<Programmation>();
            this.GetProgrammationsList();
        }

        public void GetProgrammationsList()
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
                    this.ProgrammationsList.Add(p);
                }
            }
        }
    }
}
