using Prism.Mvvm;
using WpfFestival.Events;
using Main;

namespace Main
{

    public class MainWindowViewModel :BindableBase
    {
        private string _title;
        
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public MainWindowViewModel()
        {
            _title = "Festival System";
        }
    }
}
