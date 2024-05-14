using DVS.Stores;
using DVS.ViewModels.AddViewModels;

namespace DVS.Commands.DVSViewCommands
{
    internal class OpenAddEmployeeCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
