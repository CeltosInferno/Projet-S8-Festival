using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFestival.Models
{
    public class Scene :BindableBase
    {

        private int _sceneId;
        public int Id
        {
            get { return _sceneId; }
            set { SetProperty(ref _sceneId, value); }
        }
        private string _sceneName;
        public string Nom
        {
            get { return _sceneName; }
            set { SetProperty(ref _sceneName, value); }
        }
        private int _capacity;
        public int Capacite
        {
            get { return _capacity; }
            set { SetProperty(ref _capacity, value); }//jj
        }
        private string _accessibilite;
        public string Accessibilite
        {
            get { return _accessibilite; }
            set { SetProperty(ref _accessibilite, value); }
        }
    }
}
