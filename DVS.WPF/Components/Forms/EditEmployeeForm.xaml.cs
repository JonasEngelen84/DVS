using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Components.Forms
{
    public partial class EditEmployeeForm : UserControl
    {
        public EditEmployeeForm()
        {
            InitializeComponent();
        }

        private void SwitchAvailableClothesListViewClick(object sender, RoutedEventArgs e)
        {
            if (AvailableClothesList.Visibility == Visibility.Visible)
            {
                AvailableClothesList.Visibility = Visibility.Hidden;
                DragNDropClothesLists.Visibility = Visibility.Visible;
            }
            else
            {
                AvailableClothesList.Visibility = Visibility.Visible;
                DragNDropClothesLists.Visibility = Visibility.Hidden;
            }
        }
    }
}
