using DVS.Stores;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenFilterClothesListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenFilterClothesListCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
