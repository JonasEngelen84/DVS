using DVS.WPF.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.WPF.Components.DVSListings
{
    public partial class DVSEmployeeListing : UserControl
    {
        public DVSEmployeeListing()
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
