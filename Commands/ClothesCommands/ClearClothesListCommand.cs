using DVS.Stores;

namespace DVS.Commands.ClothesCommands
{
    public class ClearClothesListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ClearClothesListCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
