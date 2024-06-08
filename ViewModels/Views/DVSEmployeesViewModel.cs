using DVS.ViewModels.ListViewItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void OnEmployeeItemClicked(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            if (border != null)
            {
                var employeeViewModel = border.DataContext as EmployeeListingItemViewModel;
                if (employeeViewModel != null)
                {
                    employeeViewModel.IsExpanded = !employeeViewModel.IsExpanded;
                }
            }
        }
    }
}
