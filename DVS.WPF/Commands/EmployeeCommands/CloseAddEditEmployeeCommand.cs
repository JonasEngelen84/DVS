using DVS.WPF.Stores;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class CloseAddEditEmployeeCommand(
        ClothesStore clothesStore,
        ModalNavigationStore modalNavigationStore)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            modalNavigationStore.Close();
        }
    }
}
