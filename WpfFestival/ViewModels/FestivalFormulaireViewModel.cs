using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfFestival.Models;
using WpfFestival.Events;


namespace WpfFestival.ViewModels
{
    public class FestivalFormulaireViewModel : BindableBase
    {
        #region Members
        private Festival _festival = new Festival();
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManaager;
        private bool _isEnabled;
        #endregion

        #region Properties
        public Festival Festival
        {
            get { return _festival; }
            set { SetProperty(ref _festival, value);
                //AddFestival.RaiseCanExecuteChanged();
            }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }
        #endregion

        public DelegateCommand<string> AddFestival { get; private set; }
        
        public FestivalFormulaireViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _regionManaager = regionManager;
            AddFestival = new DelegateCommand<string>(Executed).ObservesCanExecute(() => IsEnabled);
            
            _festival.EndDate = DateTime.Now;
            _festival.StartDate = DateTime.Now;
        }
        

        
        private void Executed(string uri)
        {
            PostFestival();
            
            _regionManaager.RequestNavigate("ContentRegion", uri);
            _eventAggregator.GetEvent<PassFestivalNameEvent>().Publish(Festival.Name.ToString());

        }

        private void PostFestival()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                Task<HttpResponseMessage> postFestivalTask = client.PostAsJsonAsync<Festival>("/api/festivals", Festival);

                postFestivalTask.Wait();

                HttpResponseMessage result1 = postFestivalTask.Result;

                //HttpResponseMessage result = client.PostAsJsonAsync("/api/festivals", obj).Result;

                if (result1.IsSuccessStatusCode)
                {
                    var readTask= result1.Content.ReadAsAsync<Festival>().Result;
                    
                }
                
            }

        }
    }
}
