using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using elasticSearch.BussinessLayer.CastleInstaller;

namespace elasticSearch.WebApi.CastleInstaller
{
    public class WindsorBootstrapper
    {
        private static IWindsorContainer _container;

        public static void CreateBootstrapContainer()
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(
                new BusinessLayerInstaller());

            _container = container;

        }

        public static void DisposeContainer()
        {
            _container.Dispose();
        }
    }
}