using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using CommonServiceLocator;

using NotifyMe.ServiceInterfaces;
using NotifyMe.Services;
using NotifyMe.ViewModels;

namespace NotifyMe
{
    public class DependencyInjector
    {
        private readonly UnityContainer _unityContainer;

        public DependencyInjector()
        {
            _unityContainer = new UnityContainer();

            #region Services
            _unityContainer.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            #endregion

            #region Popups
            _unityContainer.RegisterType<LoginPopupViewModel>();
            _unityContainer.RegisterType<PasswordPopupViewModel>();
            #endregion

            #region Alerts
            _unityContainer.RegisterType<TimeAlertsViewModel>();
            _unityContainer.RegisterType<LocationAlertsViewModel>();
            _unityContainer.RegisterType<AddTimeAlertViewModel>();
            _unityContainer.RegisterType<AddLocationAlertViewModel>();
            #endregion

            #region Locations
            _unityContainer.RegisterType<LocationsViewModel>();
            _unityContainer.RegisterType<AddLocationViewModel>();
            #endregion

            #region Other
            _unityContainer.RegisterType<SignupViewModel>();
            _unityContainer.RegisterType<HomePageMasterViewModel>();
            _unityContainer.RegisterType<SettingsViewModel>();
            _unityContainer.RegisterType<AccountViewModel>();
            #endregion

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_unityContainer));
        }

        
    }
}
