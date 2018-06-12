using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Producer.Controllers;

//[assembly: OwinStartup(typeof(Producer.Startup))]

namespace Producer
{
    public class Startup
    {
        private readonly IContainer _container;

        public Startup()
        {
            _container = BuildContainer();
        }

        public Startup(IContainer container)
        {
            _container = container;
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<WebApiControllerModule>();
            builder.RegisterModule<RealServiceModule>();
            return builder.Build();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

            app.UseAutofacMiddleware(_container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
