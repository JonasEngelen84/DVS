using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Views
{
    public partial class DVSMainView : UserControl
    {
        public DVSMainView()
        {
            InitializeComponent();
        }

        private void ClearEmployeeSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchEmployeeTextBox.Text = string.Empty;
        }

        private void ClearClothesSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClothesTextBox.Text = string.Empty;
        }
    }
}
