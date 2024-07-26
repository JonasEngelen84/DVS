using DVS.Stores;

namespace DVS.Commands
{
    public class MinusCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public MinusCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
