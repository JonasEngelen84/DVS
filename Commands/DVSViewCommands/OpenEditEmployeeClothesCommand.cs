using DVS.Stores;
using DVS.ViewModels.View_ViewModels;
using DVS.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenEditEmployeeClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditEmployeeClothesCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            EditClothesViewModel editClothesViewModel = new EditClothesViewModel(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editClothesViewModel;

            //EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel(_modalNavigationStore);
            //_modalNavigationStore.CurrentViewModel = editEmployeeViewModel;
        }
    }
}
