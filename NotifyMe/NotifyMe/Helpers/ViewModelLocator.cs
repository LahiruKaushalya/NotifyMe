using CommonServiceLocator;
using NotifyMe.ViewModels;

namespace NotifyMe.Helpers
{
    public class ViewModelLocator
    {
        #region Popup View Models
        public LoginPopupViewModel LoginPopupViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginPopupViewModel>();
            }
        }

        public PasswordPopupViewModel PasswordPopupViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PasswordPopupViewModel>();
            }
        }
        #endregion

        #region Alert View Models
        public TimeAlertsViewModel TimeAlertsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TimeAlertsViewModel>();
            }
        }

        public LocationAlertsViewModel LocationAlertsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LocationAlertsViewModel>();
            }
        }

        public AddTimeAlertViewModel AddTimeAlertViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddTimeAlertViewModel>();
            }
        }

        public AddLocationAlertViewModel AddLocationAlertViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddLocationAlertViewModel>();
            }
        }
        #endregion

        #region Location View Models
        public LocationsViewModel LocationsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LocationsViewModel>();
            }
        }

        public AddLocationViewModel AddLocationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddLocationViewModel>();
            }
        }
        #endregion

        #region Other View Models
        public MapCreationViewModel MapCreationViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MapCreationViewModel>();
            }
        }
        public SignupViewModel SignupViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SignupViewModel>();
            }
        }

        public HomePageMasterViewModel HomePageMasterViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomePageMasterViewModel>();
            }
        }

        public AccountViewModel AccountViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountViewModel>();
            }
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }
        #endregion
    }
}
