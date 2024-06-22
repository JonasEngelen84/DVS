using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.Components.Forms
{
    public partial class AddEmployeeForm : UserControl
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        //private void EmployeeClothesList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (sender is not ListView listView) return;

        //    if (listView.SelectedItem is DetailedClothesListingItem selectedItem)
        //    {
        //        DragDrop.DoDragDrop(listView, selectedItem, DragDropEffects.Move);
        //    }
        //}

        //private void AvailableClothesList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    ListView listView = sender as ListView;
        //    if (listView == null) return;

        //    var selectedItem = listView.SelectedItem as DetailedClothesListingItem;
        //    if (selectedItem != null)
        //    {
        //        DragDrop.DoDragDrop(listView, selectedItem, DragDropEffects.Move);
        //    }
        //}

        //private void EmployeeClothesList_DragOver(object sender, DragEventArgs e)
        //{
        //    if (!e.Data.GetDataPresent(typeof(DetailedClothesListingItem)))
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}

        //private void AvailableClothesList_DragOver(object sender, DragEventArgs e)
        //{
        //    if (!e.Data.GetDataPresent(typeof(DetailedClothesListingItem)))
        //    {
        //        e.Effects = DragDropEffects.None;
        //    }
        //}

        //private void EmployeeClothesList_Drop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(typeof(DetailedClothesListingItem)))
        //    {
        //        var droppedItem = e.Data.GetData(typeof(DetailedClothesListingItem)) as DetailedClothesListingItem;
        //        var viewModel = DataContext as AddEditEmployeeFormViewModel;
        //        viewModel?.AddClothesToEmployee(droppedItem);
        //    }
        //}

        //private void AvailableClothesList_Drop(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(typeof(DetailedClothesListingItem)))
        //    {
        //        var droppedItem = e.Data.GetData(typeof(DetailedClothesListingItem)) as DetailedClothesListingItem;
        //        var viewModel = DataContext as AddEditEmployeeFormViewModel;
        //        viewModel?.RemoveClothesFromEmployee(droppedItem);
        //    }
        //}
    }
}
