using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Components.ListingItems
{
    public partial class EmployeeListingItem : UserControl
    {
        public EmployeeListingItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dropdown.IsOpen = false;
        }
    }
}
