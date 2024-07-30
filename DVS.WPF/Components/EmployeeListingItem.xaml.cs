using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Components
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
