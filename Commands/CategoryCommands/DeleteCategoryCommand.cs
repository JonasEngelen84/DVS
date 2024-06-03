using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public DeleteCategoryCommand(AddEditCategoryViewModel addEditCategorieViewModel,
            ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _selectedCategoryStore = selectedCategoryStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
