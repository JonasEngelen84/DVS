using DVS.Stores;

namespace DVS.Commands.ClothesCommands
{
    public class DeleteClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public DeleteClothesCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) 
        {
            _modalNavigationStore.Close();
        }
    }
}
