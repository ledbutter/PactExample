using Autofac;
using Owin;

namespace Producer.Test
{
    public class TestStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<ProviderStateMiddleware>();

            var builder = new ContainerBuilder();
            builder.RegisterModule<WebApiControllerModule>();
            builder.RegisterModule<TestServiceModule>();
            var container = builder.Build();

            var apiStartup = new Startup(container); //This is your standard OWIN startup object
            
            apiStartup.Configuration(app);
        }
    }
}