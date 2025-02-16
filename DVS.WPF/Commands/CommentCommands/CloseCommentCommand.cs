using DVS.WPF.Stores;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.CommentCommands
{
    public class CloseCommentCommand(
        ModalNavigationStore modalNavigationStore,
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            modalNavigationStore.Close();
        }
    }
}
