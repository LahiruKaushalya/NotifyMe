using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using CommonServiceLocator;
using NotifyMe.Interfaces;
using NotifyMe.Services;
using NotifyMe.ViewModels;


namespace NotifyMe.Helpers
{
    public class DependencyInjector
    {
        private readonly UnityContainer _unityContainer;

        public DependencyInjector()
        {
            _unityContainer = new UnityContainer();

            #region Services
            _unityContainer.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IAlertService, AlertService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<ILocationService, LocationService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IValidator, Validator>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IConverter, Converter>(new ContainerControlledLifetimeManager());
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
