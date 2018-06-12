using Autofac;
using Producer.Controllers;

namespace Producer.Test
{
    public class TestServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DummyThingRetriever>().As<IThingRetriever>().InstancePerLifetimeScope();
        }
    }
}