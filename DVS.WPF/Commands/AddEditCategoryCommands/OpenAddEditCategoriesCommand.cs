﻿using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class OpenAddEditCategoriesCommand(ModalNavigationStore modalNavigationStore,
                                              CategoryStore categoryStore,
                                              AddClothesViewModel addClothesViewModel,
                                              EditClothesViewModel editClothesViewModel,
                                              AddEditClothesListingViewModel addEditListingViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly AddEditClothesListingViewModel _addEditListingViewModel = addEditListingViewModel;

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(_modalNavigationStore,
                                                                     _categoryStore,
                                                                     _addClothesViewModel,
                                                                     _editClothesViewModel,
                                                                     _addEditListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
