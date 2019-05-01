using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFestival.ViewModels.Fonctions
{
    class Styles
    {
        public List<string> StylesList { get; set; }

        public Styles()
        {
            InitialStyles();
        }

        public void InitialStyles()
        {
            StylesList = new List<string>();
            StylesList.Add("Blue");
            StylesList.Add("Folk");
            StylesList.Add("Rock");
            StylesList.Add("Pop");
        }
    }
}
