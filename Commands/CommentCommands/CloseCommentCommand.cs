using DVS.Stores;

namespace DVS.Commands.CommentCommands
{
    public class CloseCommentCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
