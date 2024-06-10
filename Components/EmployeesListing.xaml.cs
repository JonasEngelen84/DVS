using DVS.ViewModels.ListViewItems;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.Components
{
    public partial class EmployeesListing : UserControl
    {
        public EmployeesListing()
        {
            InitializeComponent();
        }

        public void OnEmployeeItemClicked(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                var viewModel = listViewItem.DataContext as EmployeeListingItemViewModel;
                if (viewModel != null)
                {
                    viewModel.IsExpanded = !viewModel.IsExpanded;
                }
            }
        }
    }
}
