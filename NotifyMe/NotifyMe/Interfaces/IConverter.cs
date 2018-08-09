using System.Collections.Generic;
using System.Collections.ObjectModel;

using NotifyMe.Models;
using NotifyMe.Models.DbModels;

namespace NotifyMe.Interfaces
{
    public interface IConverter
    {
        ObservableCollection<DisplayAlert> AlertToDisplayAlert(List<Alert> alerts);
    }
}
