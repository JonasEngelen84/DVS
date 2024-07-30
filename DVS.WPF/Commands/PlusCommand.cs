using DVS.Stores;

namespace DVS.Commands
{
    public class PlusCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public PlusCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
