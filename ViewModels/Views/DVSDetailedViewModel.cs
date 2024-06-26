﻿using DVS.Commands.DVSDetailedViewCommands;
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
        public ICommand OpenEditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }

        public DVSDetailedViewModel(
            ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore, ClothesStore clothesStore,
            EmployeeStore employeeStore, SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore)
        {
            DVSDetailedClothesListingView = new(clothesStore, employeeStore);
            DVSDetailedEmployeesListingView = new(clothesStore, employeeStore);

            SaveCommand = new SaveCommand(modalNavigationStore);
            OpenFilterClothesListCommand = new OpenFilterClothesListCommand(modalNavigationStore);
            OpenFilterEmployeeListCommand = new OpenFilterEmployeeListCommand(modalNavigationStore);
            PlusCommand = new PlusCommand(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);
            MinusCommand = new MinusCommand(selectedClothesStore, selectedEmployeeClothesStore, modalNavigationStore);

            OpenAddEmployeeCommand = new OpenAddEmployeeCommand(
                DVSDetailedClothesListingView, clothesStore, employeeStore, modalNavigationStore);

            OpenAddClothesCommand = new OpenAddClothesCommand(
                modalNavigationStore, categoryStore, seasonStore,
                selectedCategoryStore, selectedSeasonStore, clothesStore);

            OpenEditCommand = new OpenEditCommand(
                DVSDetailedClothesListingView, modalNavigationStore, categoryStore,
                seasonStore, selectedCategoryStore, selectedSeasonStore, selectedClothesStore,
                selectedEmployeeClothesStore, clothesStore, employeeStore);


        }
    }
}
