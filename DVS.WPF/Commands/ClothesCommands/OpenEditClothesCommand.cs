﻿using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class OpenEditClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
                                        ModalNavigationStore modalNavigationStore,
                                        SizeStore sizeStore,
                                        CategoryStore categoryStore,
                                        SeasonStore seasonStore,
                                        ClothesStore clothesStore,
                                        ClothesSizeStore clothesSizeStore,
                                        EmployeeClothesSizesStore employeeClothesSizesStore,
                                        EmployeeStore employeeStore,
                                        DVSListingViewModel dVSListingViewModel)
                                        : CommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override void Execute(object parameter)
        {
            Clothes _clothes = _clothesListingItemViewModel.Clothes;

            EditClothesViewModel EditClothesViewModel = new(_clothes,
                                                            _modalNavigationStore,
                                                            _sizeStore,
                                                            _categoryStore,
                                                            _seasonStore,
                                                            _clothesStore,
                                                            _clothesSizeStore,
                                                            _employeeClothesSizesStore,
                                                            _employeeStore,
                                                            _dVSListingViewModel);

            _modalNavigationStore.CurrentViewModel = EditClothesViewModel;
        }
    }
}
