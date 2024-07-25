using DVS.Stores;

namespace DVS.Commands.AddEditEmployeeCommands
{
    public class CloseAddEditEmployeeCommand(ClothesStore clothesStore,
        ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _clothesStore.Load();
            _modalNavigationStore.Close();
        }
    }
}
