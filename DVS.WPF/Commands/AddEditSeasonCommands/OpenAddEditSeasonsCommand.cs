using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore, SizeStore sizeStore,
        CategoryStore categoryStore, SeasonStore seasonStore, Clothes clothes, AddClothesViewModel addClothesViewModel,
        EditClothesViewModel editClothesViewModel, AddEditListingViewModel addEditListingViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly Clothes _clothes = clothes;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly AddEditListingViewModel _addEditListingViewModel = addEditListingViewModel;

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(_modalNavigationStore, _categoryStore, _seasonStore,
                _clothes, _sizeStore, _addClothesViewModel, _editClothesViewModel, _addEditListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
