using DVS.Stores;
using DVS.ViewModels;

namespace DVS.Commands.CommentCommands
{
    public class CloseCommentCommand(ModalNavigationStore modalNavigationStore,
        DVSListingViewModel dVSListingViewModel) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
