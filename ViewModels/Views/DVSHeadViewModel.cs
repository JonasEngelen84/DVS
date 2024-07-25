using DVS.Commands.DVSDetailedViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class DVSHeadViewModel(DVSListingViewModel dVSListingViewModel,
                                  ModalNavigationStore modalNavigationStore,
                                  CategoryStore categoryStore,
                                  SeasonStore seasonStore,
                                  ClothesStore clothesStore,
                                  EmployeeStore employeeStore) : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(dVSListingViewModel, employeeStore, clothesStore, modalNavigationStore);
        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
    }
}
