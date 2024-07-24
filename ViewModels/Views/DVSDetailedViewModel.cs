using DVS.Commands.DVSDetailedViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class DVSDetailedViewModel : ViewModelBase
    {
        public DVSListingViewModel DVSDetailedClothesListingView { get; }

        public ICommand OpenFilterClothesListCommand { get; }
        public ICommand OpenFilterEmployeeListCommand { get; }
        public ICommand OpenAddEmployeeCommand { get; }
        public ICommand OpenAddClothesCommand { get; }
        public ICommand OpenCommentCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSDetailedViewModel(DVSListingViewModel dVSListingViewModel,
                                    ModalNavigationStore modalNavigationStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    ClothesStore clothesStore,
                                    EmployeeStore employeeStore,
                                    SelectedClothesStore selectedClothesStore)
        {
            DVSDetailedClothesListingView = dVSListingViewModel;

            OpenAddEmployeeCommand = new OpenAddEmployeeCommand(DVSDetailedClothesListingView, employeeStore, modalNavigationStore);
            OpenAddClothesCommand = new OpenAddClothesCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
            MinusCommand = new MinusCommand(selectedClothesStore, modalNavigationStore);
            PlusCommand = new PlusCommand(selectedClothesStore, modalNavigationStore);
            OpenCommentCommand = new OpenCommentCommand(DVSDetailedClothesListingView, modalNavigationStore);
            OpenFilterEmployeeListCommand = new OpenFilterEmployeeListCommand(modalNavigationStore);
            OpenFilterClothesListCommand = new OpenFilterClothesListCommand(modalNavigationStore);
            SaveCommand = new SaveCommand(modalNavigationStore);
        }
    }
}
