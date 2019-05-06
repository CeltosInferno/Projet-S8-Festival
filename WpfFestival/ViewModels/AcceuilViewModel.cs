using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;
using WpfFestival.Events;

namespace WpfFestival.ViewModels
{
    public class AcceuilViewModel : BindableBase
    {
        private readonly IRegionManager _regionManger;
        private readonly IEventAggregator _eventAggregator;

        public int  OrganisateurId {get;set;}
        public DelegateCommand<string> GoToFestivalFormulaire { get; private set; }
        public DelegateCommand<string> GoToGestionFestival { get; private set; }
        public DelegateCommand<string> GoToGestionScene { get; private set; }
        public DelegateCommand<string> GoToGestionArtiste { get; private set; }

        public AcceuilViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) {

            _regionManger = regionManager;
            _eventAggregator = eventAggregator;
            GoToFestivalFormulaire = new DelegateCommand<string>(NavigateAndPassId);
            GoToGestionFestival = new DelegateCommand<string>(NavigateAndRefreshAndPassId);
            GoToGestionScene = new DelegateCommand<string>(NavigateAndRefresh);
            GoToGestionArtiste = new DelegateCommand<string>(NavigateAndRefresh);
            //_eventAggregator.GetEvent<PassOrganisateurIdEvent>().Subscribe(GetOrganisateurId);

        }

        #region Events
        private void Navigate(string uri)
        {
            if (uri != null)
            {
                _regionManger.RequestNavigate("ContentRegion", uri);
            }

        }
        private void NavigateAndRefresh(string uri)
        {
            if (uri != null)
            {
                _regionManger.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste
            }

        }
        private void NavigateAndPassId(string uri)
        {
            if (uri != null)
            {
                _regionManger.RequestNavigate("ContentRegion", uri);
               // _eventAggregator.GetEvent<PassOrganisateurIdEvent>().Publish(OrganisateurId);
            }
        }
        private void NavigateAndRefreshAndPassId(string uri)
        {
            if (uri != null)
            {
                _regionManger.RequestNavigate("ContentRegion", uri);
                _eventAggregator.GetEvent<RefreshEvent>().Publish(true); //Rafrachir la liste
               // _eventAggregator.GetEvent<PassOrganisateurIdEvent>().Publish(OrganisateurId);
            }
        }

        private void GetOrganisateurId(int obj) { OrganisateurId = obj; }
        #endregion

    }
}
