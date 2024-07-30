using DVS.WPF.Stores;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.CommentCommands
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
