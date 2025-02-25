using DVS.WPF.ViewModels.ListingItems;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.WPF.Components.DVSListings

{
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
