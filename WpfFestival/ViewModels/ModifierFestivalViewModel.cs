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

namespace WpfFestival.ViewModels
{
    public class ModifierFestivalViewModel : BindableBase
    {
        #region Members
        private readonly IEventAggregator _eventAggregator;
        private Festival _festival;
        private bool _isEnabled;

        #endregion

        #region Properties
        public Festival Festival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value); }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        #endregion

        #region Command
        public DelegateCommand ModifierFestival { get; private set; }

        private void Executed()
        {
            PutFestival($"/api/{Festival.Id}");
        }

        #endregion

        public ModifierFestivalViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Festival = new Festival();
            ModifierFestival = new DelegateCommand(Executed).ObservesCanExecute(()=>IsEnabled);
            _eventAggregator.GetEvent<PassFestivalEvent>().Subscribe(PassFestival);
        }

        private void PassFestival(Festival obj)
        {
            Festival = obj;
        }

        public bool PutFestival(string uri)
        {
            using (var client = new HttpClient())
            {   
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> putFestivalTask = client.PutAsJsonAsync<Festival>(uri, Festival);

                putFestivalTask.Wait();

                HttpResponseMessage result = putFestivalTask.Result;

                //HttpResponseMessage result = client.PostAsJsonAsync("/api/festivals", obj).Result;

                if (result.IsSuccessStatusCode)
                {
                    return true;

                }
                return false;

            }
        }

    }
}
