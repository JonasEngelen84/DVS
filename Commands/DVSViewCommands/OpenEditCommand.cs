using DVS.Stores;
using DVS.ViewModels.View_ViewModels;
using DVS.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly SelectedClothesStore selectedClothesStore;
        private readonly SelectedEmployeeClothesStore selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditCommand(SelectedClothesStore selectedClothesStore, SelectedEmployeeClothesStore selectedEmployeeClothesStore, ModalNavigationStore modalNavigationStore)
        {
            this.selectedClothesStore = selectedClothesStore;
            this.selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        int i = 0;
        public override void Execute(object parameter)
        {
            if(i%2 == 0)
            {
                EditClothesViewModel editClothesViewModel = new EditClothesViewModel(_modalNavigationStore);
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
