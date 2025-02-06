using DVS.WPF.Stores;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class CloseAddEditClothesCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
