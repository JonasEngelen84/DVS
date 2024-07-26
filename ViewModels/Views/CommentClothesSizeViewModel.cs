using DVS.Commands.CommentCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class CommentClothesSizeViewModel : ViewModelBase
    {
        public CommentClothesSizeFormViewModel CommentClothesSizeFormViewModel { get; }
        public ICommand CloseComment { get; }

        public CommentClothesSizeViewModel(ModalNavigationStore modalNavigationStore, ClothesStore clothesStore,
            SelectedDetailedClothesItemStore selectedDetailedClothesItemStore)
        {
            ICommand SubmitComment = new SubmitCommentClothesSizeCommand(this, clothesStore, modalNavigationStore);
            CloseComment = new CloseCommentCommand(modalNavigationStore);

            CommentClothesSizeFormViewModel = new(selectedDetailedClothesItemStore, SubmitComment);
        }
    }
}
