using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace WpfFestival.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManger;

        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public DelegateCommand<string> GoToFestivalFormulaire { get; private set; }
        public DelegateCommand<string> GoToProgrammationFormulaire { get; private set; }
        public DelegateCommand<string> GoToModifierScene { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager) {

            _regionManger = regionManager;

            GoToFestivalFormulaire = new DelegateCommand<string>(Navigate);
            GoToProgrammationFormulaire = new DelegateCommand<string>(Navigate);
            GoToModifierScene = new DelegateCommand<string>(Navigate);
        }

        public void Navigate(string uri)
        {   if(uri !=null)
                _regionManger.RequestNavigate("ContentRegion", uri);
        }
    }
}
