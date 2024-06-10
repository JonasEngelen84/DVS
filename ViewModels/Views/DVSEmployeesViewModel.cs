using DVS.ViewModels.ListViewItems;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class DVSEmployeesViewModel : ViewModelBase
    {
        public EmployeesListingViewModel EmployeesListingViewModel { get; }

        public DVSEmployeesViewModel()
        {
            EmployeesListingViewModel = new EmployeesListingViewModel();
        }

        
    }
}
