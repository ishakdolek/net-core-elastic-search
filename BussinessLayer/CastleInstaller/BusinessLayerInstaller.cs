using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace elasticSearch.BussinessLayer.CastleInstaller
{
    public class BusinessLayerInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("elasticSearch.BussinessLayer").Pick().WithService.DefaultInterfaces()
                .LifestyleTransient());

        }
    }
}