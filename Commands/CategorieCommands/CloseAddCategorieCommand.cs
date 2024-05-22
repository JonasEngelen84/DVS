﻿using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategorieCommands
{
    public class CloseAddCategorieCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public CloseAddCategorieCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
