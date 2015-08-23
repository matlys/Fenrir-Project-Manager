using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using DataAccessImplementation;
using DataAccessInterfaces;
using FenrirProjectManager.Controllers;
namespace FenrirProjectManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        protected void Application_Start()
        {
            // build container
            _containerBuilder = new ContainerBuilder();
            
            // register controllers
            _containerBuilder.RegisterType<HomeController>().InstancePerRequest();
            _containerBuilder.RegisterType<AccountController>().InstancePerRequest();
            _containerBuilder.RegisterType<ManageController>().InstancePerRequest();
            _containerBuilder.RegisterType<UsersController>().InstancePerRequest();
            _containerBuilder.RegisterType<ProjectsController>().InstancePerRequest();
            _containerBuilder.RegisterType<IssuesController>().InstancePerRequest();

            //register services
            _containerBuilder.RegisterType<IssueRepo>().AsSelf().As<IIssueRepo>();
            _containerBuilder.RegisterType<ProjectRepo>().AsSelf().As<IProjectRepo>();
            _containerBuilder.RegisterType<UserRepo>().AsSelf().As<IUserRepo>();

            // building controllers
            _container = _containerBuilder.Build();

            // dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
