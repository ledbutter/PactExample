using Autofac;
using Autofac.Integration.WebApi;
using Producer.Controllers;

namespace Producer
{
    public class RealServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RealThingRetriever>().As<IThingRetriever>().InstancePerLifetimeScope();
        }
    }
}