using DVS.ViewModels.ListViewItems;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.Components
{
    /// <summary>
    /// Interaktionslogik für DVSClothesListing.xaml
    /// </summary>
    public partial class DVSClothesListing : UserControl
    {
        public DVSClothesListing()
        {
            InitializeComponent();
        }

        //TODO: OnClothesItemClicked beschreiben
        public void OnClothesItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                if (listViewItem.DataContext is ClothesListingItemViewModel viewModel)
                {
                    viewModel.IsExpanded = !viewModel.IsExpanded;
                }
            }
        }
    }
}
