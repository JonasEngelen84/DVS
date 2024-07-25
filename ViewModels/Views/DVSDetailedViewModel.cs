using DVS.Commands.DVSDetailedViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class DVSDetailedViewModel(DVSListingViewModel dVSListingViewModel,
                                      ModalNavigationStore modalNavigationStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore,
                                      EmployeeStore employeeStore,
                                      SelectedClothesStore selectedClothesStore) : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenFilterClothesListCommand { get; } = new OpenFilterClothesListCommand(modalNavigationStore);
        public ICommand OpenFilterEmployeeListCommand { get; } = new OpenFilterEmployeeListCommand(modalNavigationStore);
        public ICommand OpenAddEmployeeCommand { get; } = new OpenAddEmployeeCommand(dVSListingViewModel, employeeStore, clothesStore, modalNavigationStore);
        public ICommand OpenAddClothesCommand { get; } = new OpenAddClothesCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
        public ICommand OpenCommentCommand { get; } = new OpenCommentCommand(dVSListingViewModel, modalNavigationStore);
        public ICommand SaveCommand { get; } = new SaveCommand(modalNavigationStore);
        public ICommand PlusCommand { get; } = new PlusCommand(selectedClothesStore, modalNavigationStore);
        public ICommand MinusCommand { get; } = new MinusCommand(selectedClothesStore, modalNavigationStore);
    }
}
