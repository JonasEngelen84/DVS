﻿using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class OpenAddEditSeasonsCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _selectedCategoryStore = selectedCategoryStore;
        }

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(_modalNavigationStore, _selectedCategoryStore);
            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
