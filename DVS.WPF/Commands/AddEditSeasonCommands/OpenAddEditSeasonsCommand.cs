using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore,
                                           SeasonStore seasonStore,
                                           AddClothesViewModel addClothesViewModel,
                                           EditClothesViewModel editClothesViewModel,
                                           AddEditListingViewModel addEditListingViewModel)
                                           : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly AddEditListingViewModel _addEditListingViewModel = addEditListingViewModel;

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(_modalNavigationStore,
                                                                _seasonStore,
                                                                _addClothesViewModel,
                                                                _editClothesViewModel,
                                                                _addEditListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
