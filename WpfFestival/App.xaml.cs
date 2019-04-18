using System.Windows;
using WpfFestival.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;

namespace WpfFestival
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FestivalFormulaire>();
            containerRegistry.RegisterForNavigation<ProgrammationFormulaire>();
            containerRegistry.RegisterForNavigation<ModifierScene>();
        }


        
    }
}
