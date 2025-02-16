﻿using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class CloseAddEditSeasonCommand(
        ModalNavigationStore modalNavigationStore,
        AddClothesViewModel addClothesViewModel,
        EditClothesViewModel editClothesViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            modalNavigationStore.CurrentViewModel =
                addClothesViewModel != null ? addClothesViewModel : editClothesViewModel;
        }
    }
}
