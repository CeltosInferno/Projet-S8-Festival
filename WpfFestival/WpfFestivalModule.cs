using WpfFestival.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace WpfFestival
{
    public class WpfFestivalModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("MainContentRegion", typeof(Identification));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Acceuil>();

            containerRegistry.RegisterForNavigation<FestivalFormulaire>();
            containerRegistry.RegisterForNavigation<ArtisteFormulaire>();
            containerRegistry.RegisterForNavigation<ProgrammationFormulaire>();
            containerRegistry.RegisterForNavigation<SceneFormulaire>();
            containerRegistry.RegisterForNavigation<ModifierScene>();
            containerRegistry.RegisterForNavigation<ModifierArtiste>();
            containerRegistry.RegisterForNavigation<ModifierProgrammation>();
            containerRegistry.RegisterForNavigation<ModifierFestival>();
            containerRegistry.RegisterForNavigation<GestionFestival>();
            containerRegistry.RegisterForNavigation<GestionScene>();
            containerRegistry.RegisterForNavigation<GestionArtiste>();
        }
    }
}