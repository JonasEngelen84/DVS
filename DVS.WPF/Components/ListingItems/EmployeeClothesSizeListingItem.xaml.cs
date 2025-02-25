using DVS.WPF.ViewModels.ListingItems;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.WPF.Components.ListingItems
{
    public partial class EmployeeClothesSizeListingItem : UserControl
    {
        public EmployeeClothesSizeListingItem()
        {
            InitializeComponent();
        }

        private void EmployeeClothesSizeListingItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (DataContext is EmployeeClothesSizeListingItemViewModel dataContext)
                {
                    DataObject dragData = new("EmployeeClothesSize", dataContext);
                    DragDrop.DoDragDrop(this, dragData, DragDropEffects.Move);
                }
            }
        }
    }
}
