using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategoryCommands
{
    public class AddCategoryCommand : CommandBase
    {
        private AddEditCategoryViewModel _addEditCategoryViewModel { get; }
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public AddCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel,
            ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            _addEditCategoryViewModel = addEditCategoryViewModel;
            _modalNavigationStore = modalNavigationStore;
            _selectedCategoryStore = selectedCategoryStore;
        }

        public override void Execute(object parameter)
        {
            //_addEditCategoryViewModel.AddEditCategoryFormViewModel
        }
    }
}
