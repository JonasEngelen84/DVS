using DVS.WPF.ViewModels.ListingItems;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (e.OriginalSource is DependencyObject sourceElement)
            {
                // Falls Textbox oder deren Child getroffen wurde Drag abbrechen
                if (FindAncestor<TextBox>(sourceElement) != null) return;
            }

            StartDrag();
        }

        private void StartDrag()
        {
            if (DataContext is EmployeeClothesSizeListingItemViewModel dataContext)
            {
                DataObject dragData = new("EmployeeClothesSize", dataContext);
                DragDrop.DoDragDrop(this, dragData, DragDropEffects.Move);
            }
        }

        private static T? FindAncestor<T>(DependencyObject? current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T t) return t;
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}
