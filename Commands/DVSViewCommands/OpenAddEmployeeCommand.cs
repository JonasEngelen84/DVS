using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenAddEmployeeCommand : CommandBase
    {
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEmployeeCommand(ClothesStore clothesStore,
                                    EmployeeStore employeeStore,
                                    ModalNavigationStore modalNavigationStore)
        {
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(_clothesStore,
                                                            _employeeStore,
                                                            _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
