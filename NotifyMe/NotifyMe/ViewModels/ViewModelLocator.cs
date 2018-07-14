using CommonServiceLocator;

namespace NotifyMe.ViewModels
{
    public class ViewModelLocator
    {
        public LoginViewModel LoginViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
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
    }
}
