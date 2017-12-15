using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation;
using crmManager.BusinessLayer.CasttleInstaller;
using crmManager.WebSwitch.CasttleInstaller;

namespace crmManager.WebApi.Windsor
{
    public class DependencyConventions : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(
                new BusinessLayerInstaller(),
                new SwitchInstaller(),
                FromAssembly.Named("crmManager.DataLayer")
                );

            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().WithServiceSelf().LifestylePerWebRequest());
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(AbstractValidator<>)).WithServiceFirstInterface().LifestylePerWebRequest());

            container.Register(
                Classes.FromThisAssembly()
                    .IncludeNonPublicTypes()
                    .Where(x => x != typeof (int)).WithService.DefaultInterfaces()
                );
        }
    }
 }