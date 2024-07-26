using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditSeasonCommands
{
    public class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
        SeasonStore seasonStore, ClothesModel clothes, AddClothesViewModel addClothesViewModel,
        EditClothesViewModel editClothesViewModel, AddEditListingViewModel addEditListingViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesModel _clothes = clothes;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly AddEditListingViewModel _addEditListingViewModel = addEditListingViewModel;

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(_modalNavigationStore, _categoryStore, _seasonStore,
                _clothes, _addClothesViewModel, _editClothesViewModel, _addEditListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
