using System.Windows;
using WpfFestival.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Prism.Regions;

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

        //protected void OnInitialized(IContainerProvider containerProvider)
        //{
        //    var regionManager = containerProvider.Resolve<IRegionManager>();

        //    regionManager.RegisterViewWithRegion("ScenesListRegion", typeof(ScenesList));
        //}

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FestivalFormulaire>();
            containerRegistry.RegisterForNavigation<ProgrammationFormulaire>();
            containerRegistry.RegisterForNavigation<ModifierScene>();
            containerRegistry.RegisterForNavigation<ModifierArtiste>();
            containerRegistry.RegisterForNavigation<ModifierProgrammation>();
            containerRegistry.RegisterForNavigation<Acceuil>();

        }
        

    }
}
