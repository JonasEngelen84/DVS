using DVS.Commands.DVSDetailedViewCommands;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class DVSDetailedViewModel : ViewModelBase
    {
        public DVSListingViewModel DVSDetailedClothesListingView { get; }
        public DVSListingViewModel DVSDetailedEmployeesListingView { get; }

        public ICommand OpenFilterClothesListCommand { get; }
        public ICommand OpenFilterEmployeeListCommand { get; }
        public ICommand OpenAddEmployeeCommand { get; }
        public ICommand OpenAddClothesCommand { get; }
        public ICommand OpenCommentCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSDetailedViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, ClothesStore clothesStore, EmployeeStore employeeStore,
            SelectedClothesStore selectedClothesStore, SelectedEmployeeClothesStore selectedEmployeeClothesStore)
        {
            DVSDetailedClothesListingView = new(
                clothesStore, employeeStore, modalNavigationStore, categoryStore, seasonStore);

            DVSDetailedEmployeesListingView = new(
                clothesStore, employeeStore, modalNavigationStore, categoryStore, seasonStore);

            SaveCommand = new SaveCommand(modalNavigationStore);

            OpenFilterClothesListCommand = new OpenFilterClothesListCommand(modalNavigationStore);

            OpenFilterEmployeeListCommand = new OpenFilterEmployeeListCommand(modalNavigationStore);

            PlusCommand = new PlusCommand(
                selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);

            MinusCommand = new MinusCommand(
                selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);

            OpenAddEmployeeCommand = new OpenAddEmployeeCommand(
                DVSDetailedClothesListingView, employeeStore, modalNavigationStore);

            OpenAddClothesCommand = new OpenAddClothesCommand(
                modalNavigationStore, categoryStore, seasonStore, clothesStore);

            OpenCommentCommand = new OpenCommentCommand(
                DVSDetailedClothesListingView, DVSDetailedEmployeesListingView, modalNavigationStore);


        }
    }
}
