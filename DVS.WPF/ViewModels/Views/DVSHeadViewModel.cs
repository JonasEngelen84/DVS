﻿using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.WPF.Commands.AddEditEmployeeCommands;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class DVSHeadViewModel(DVSListingViewModel dVSListingViewModel,
                                  ModalNavigationStore modalNavigationStore,
                                  SizeStore sizeStore,
                                  CategoryStore categoryStore,
                                  SeasonStore seasonStore,
                                  ClothesStore clothesStore,
                                  EmployeeStore employeeStore)
                                  : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(
            dVSListingViewModel, employeeStore, clothesStore, modalNavigationStore);

        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(
            modalNavigationStore, sizeStore, categoryStore, seasonStore, clothesStore);
    }
}
