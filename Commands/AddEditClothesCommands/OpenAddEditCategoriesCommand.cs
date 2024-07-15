using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditClothesCommands
{
    public class OpenAddEditCategoriesCommand( ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
        AddClothesViewModel addClothesViewModel, EditClothesViewModel editClothesViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(
                _modalNavigationStore, _categoryStore, _addClothesViewModel, _editClothesViewModel);

            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
