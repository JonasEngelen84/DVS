using DVS.ViewModels.ListViewItems;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.Components
{
    public partial class DVSEmployeesListing : UserControl
    {
        public DVSEmployeesListing()
        {
            InitializeComponent();
        }

        //TODO: OnEmployeeItemClicked beschreiben
        public void OnEmployeeItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                if (listViewItem.DataContext is EmployeeListingItemViewModel viewModel)
                {
                    viewModel.IsExpanded = !viewModel.IsExpanded;
                }
            }
        }
    }
}
