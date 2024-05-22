using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
