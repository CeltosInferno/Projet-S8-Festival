using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using WpfFestival.Models;

namespace WpfFestival.Events
{
    
    public class PassFestivalNameEvent : PubSubEvent<string>
    {
    }

    //public class PassSceneIdEvent : PubSubEvent<int>
    //{
    //}
}
