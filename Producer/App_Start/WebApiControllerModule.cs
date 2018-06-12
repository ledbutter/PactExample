using Autofac;
using Autofac.Integration.WebApi;
using Producer.Controllers;

namespace Producer
{
    public class WebApiControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(TheThingController).Assembly).InstancePerRequest();
        }
    }
}