using System;
using System.Data;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MvcStarterKit.Abstractions;
using MvcStarterKit.Services;

namespace MvcStarterKit
{
    /// <summary>
    /// Bootstrapper for application initialization. Keep it simple.
    /// </summary>
    public sealed class Bootstrapper : IDisposable
    {
        private readonly IWindsorContainer _container;
        private static Bootstrapper _currentApplication;

        public Bootstrapper(params IWindsorInstaller[] installers)
        {
            _container = new WindsorContainer().Install(installers);

            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static Bootstrapper StartApplication()
        {
            return StartApplication(new IWindsorInstaller[] { new ControllerInstaller(), new RepositoryInstaller() });
        }

        public static Bootstrapper StartApplication(IWindsorInstaller[] installers)
        {
            _currentApplication = new Bootstrapper(installers);
            return _currentApplication;
        }

        public Bootstrapper InitializeDependencies(Action<IWindsorContainer> containerRegistrationAction = null)
        {
            _container.Register(Component.For<ISessionService>().ImplementedBy<AspNetSessionService>());
            if (containerRegistrationAction != null)
            {
                containerRegistrationAction(_container);
            }
            return this;
        }

        public Bootstrapper InstallDatabase(Action<IDbConnection> dbCreationAction = null)
        {
            if (dbCreationAction != null)
            {
                DbHelper.Execute(dbCreationAction);
            }
            return this;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public static T GetService<T>()
        {
            return _currentApplication._container.Resolve<T>();
        }
    }
}