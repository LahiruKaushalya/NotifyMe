using CommonServiceLocator;

namespace NotifyMe.ViewModels
{
    public class ViewModelLocator
    {
        public LoginPopupViewModel LoginPopupViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginPopupViewModel>();
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

        public PasswordPopupViewModel PasswordPopupViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PasswordPopupViewModel>();
            }
        }
    }
}
