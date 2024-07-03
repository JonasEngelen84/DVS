using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : AsyncCommandBase
    {
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public DeleteCategoryCommand(AddEditCategoryViewModel addEditCategorieViewModel,
            SelectedCategoryStore selectedCategoryStore)
        {
            _selectedCategoryStore = selectedCategoryStore;
        }

        public override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
