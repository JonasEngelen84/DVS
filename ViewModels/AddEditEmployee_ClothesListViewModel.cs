using DVS.Models;

namespace DVS.ViewModels
{
    public class AddEditEmployee_ClothesListViewModel : ViewModelBase
    {
        public DVSDetailedClothesListingViewModel DVSDetailedClothesListingViewModel { get; }

        public AddEditEmployee_ClothesListViewModel(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel)
        {
            DVSDetailedClothesListingViewModel = dVSDetailedClothesListingViewModel;
        }
    }
}
