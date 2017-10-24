using Autofac;
using Autofac.Integration.Mvc;
using MagneticNote.BLL;
using MagneticNote.EFDAL;
using MagneticNote.Model.Entity;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace MagneticNote
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static String KEY = ConfigurationManager.AppSettings["RequestKey"];
        protected void Application_Start()
        {
            AutoFacRigster();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        protected void Application_BeginRequest()
        {
            //if (Request.Url.Scheme.Equals("http"))
            //{
            //    Response.Redirect(Request.Url.Scheme + "s://" + Request.Url.Authority + Request.Url.PathAndQuery);
            //    Response.End();
            //}
            String key = Request["RequestKey"];
            if (key == null || !key.Equals(KEY))
            {
                Response.Write("You have no authorization.Please contact your system administrator.");
                Response.End();
            }
        }

        private void AutoFacRigster()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(UserBLL).Assembly).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterAssemblyTypes(typeof(UserDAL).Assembly).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterAssemblyTypes(typeof(MagneticNoteContainer).Assembly).InstancePerRequest();
            // ...or you can register individual controlllers manually.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            Context.Cache["contanier"] = container;
        }
    }
}
