using DVS.Models;

namespace DVS.ViewModels
{
    public class AddEditEmployee_ClothesListViewModel : ViewModelBase
    {
        public DVSDetailedClothesListingViewModel DVSDetailedClothesListingViewModel { get; }

        public List<ClothesModel> Clothes;

        public AddEditEmployee_ClothesListViewModel(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel)
        {
            DVSDetailedClothesListingViewModel = dVSDetailedClothesListingViewModel;
            Clothes =
            [
                new ClothesModel("666", "Schuhe", null, null, null)
            ];
        }
    }
}
