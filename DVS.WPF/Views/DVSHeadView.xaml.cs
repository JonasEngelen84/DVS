using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Views
{
    public partial class DVSHeadView : UserControl
    {
        public DVSHeadView()
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
