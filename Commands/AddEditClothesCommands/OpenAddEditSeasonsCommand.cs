using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditClothesCommands
{
    public class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore,SeasonStore seasonStore,
        AddClothesViewModel addClothesViewModel, EditClothesViewModel editClothesViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(
                _modalNavigationStore, _seasonStore, _addClothesViewModel, _editClothesViewModel);

            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
