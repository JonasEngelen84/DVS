using DVS.Stores;

namespace DVS.ViewModels.Views
{
    public class DVSHeadViewModel : ViewModelBase
    {
        public DVSListingViewModel DVSClothesListingViewModel { get; }
        public DVSListingViewModel DVSEmployeeListingViewModel { get; }

        public DVSHeadViewModel(EmployeeStore employeeStore, ClothesStore clothesStore)
        {
            DVSClothesListingViewModel = new(clothesStore, employeeStore);
            DVSEmployeeListingViewModel = new(clothesStore, employeeStore);
        }

    }
}
