using DVS.Commands.DVSViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class DVSViewModel : ViewModelBase
    {
        public EmployeesClothesListViewViewModel EmployeesClothesListViewViewModel { get; }
        public ClothesListViewViewModel ClothesListViewViewModel { get; }

        public ICommand OpenFilterClothesListCommand { get; }
        public ICommand OpenFilterEmployeeListCommand { get; }
        public ICommand OpenAddEmployeeCommand { get; }
        public ICommand OpenAddClothesCommand { get; }
        public ICommand OpenEditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSViewModel(SelectedClothesStore selectedClothesStore, SelectedEmployeeClothesStore selectedEmployeeClothesStore,
            SelectedCategoryStore selectedCategoryStore, SelectedSeasonStore selectedSeasonStore, ModalNavigationStore modalNavigationStore)
        {
            ClothesListViewViewModel = new(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);
            EmployeesClothesListViewViewModel = new(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore, ClothesListViewViewModel);

            OpenFilterClothesListCommand = new OpenFilterClothesListCommand(modalNavigationStore);
            OpenFilterEmployeeListCommand = new OpenFilterEmployeeListCommand(modalNavigationStore);
            OpenAddEmployeeCommand = new OpenAddEmployeeCommand(modalNavigationStore);
            OpenAddClothesCommand = new OpenAddClothesCommand(modalNavigationStore, selectedCategoryStore);
            OpenEditCommand = new OpenEditCommand(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore, selectedCategoryStore);
            SaveCommand = new SaveCommand(modalNavigationStore);
            PlusCommand = new PlusCommand(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);
            MinusCommand = new MinusCommand(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);
        }
    }
}
