using DVS.Stores;
using DVS.ViewModels.View_ViewModels;
using DVS.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public OpenEditCommand(SelectedClothesStore selectedClothesStore, SelectedEmployeeClothesStore selectedEmployeeClothesStore,
            ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;
            _selectedCategoryStore = selectedCategoryStore;
        }

        int i = 0;
        public override void Execute(object parameter)
        {
            if(i%2 == 0)
            {
                EditClothesViewModel editClothesViewModel = new EditClothesViewModel(_modalNavigationStore, _selectedCategoryStore);
                _modalNavigationStore.CurrentViewModel = editClothesViewModel;
            }
            else
            {
                EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel(_modalNavigationStore);
                _modalNavigationStore.CurrentViewModel = editEmployeeViewModel;
            }

            i++;
        }
    }
}
