using DVS.WPF.Stores;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class CloseAddEditEmployeeCommand(ClothesStore clothesStore,
        ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
