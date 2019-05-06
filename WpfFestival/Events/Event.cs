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
    public class PassFestivalEvent : PubSubEvent<Festival>
    {
    }
    public class PassProgrammationEvent : PubSubEvent<Programmation>
    {
    }
    public class PassSceneEvent : PubSubEvent<Scene>
    { 
    }
    public class PassArtisteEvent : PubSubEvent<Artiste> { }
    public class RefreshEvent : PubSubEvent<bool> { }
    //public class PassOrganisateurIdEvent : PubSubEvent<int> { }
    //public class PassSceneIdEvent : PubSubEvent<int>
    //{
    //}
}
