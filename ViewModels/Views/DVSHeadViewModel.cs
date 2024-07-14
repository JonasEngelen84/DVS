using DVS.Stores;

namespace DVS.ViewModels.Views
{
    public class DVSHeadViewModel : ViewModelBase
    {
        public DVSListingViewModel DVSClothesListingViewModel { get; }
        public DVSListingViewModel DVSEmployeeListingViewModel { get; }

        public DVSHeadViewModel(ClothesStore clothesStore, EmployeeStore employeeStore,
            ModalNavigationStore modalNavigationStore, CategoryStore categoryStore, SeasonStore seasonStore)
        {
            DVSClothesListingViewModel = new(
                clothesStore, employeeStore, modalNavigationStore, categoryStore, seasonStore);

            DVSEmployeeListingViewModel = new(
                clothesStore, employeeStore, modalNavigationStore, categoryStore, seasonStore);
        }

    }
}
