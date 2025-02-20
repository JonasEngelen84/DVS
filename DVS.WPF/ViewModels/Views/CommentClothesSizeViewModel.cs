using DVS.WPF.Commands.CommentCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class CommentClothesSizeViewModel : ViewModelBase
    {
        public CommentClothesSizeFormViewModel CommentClothesSizeFormViewModel { get; }
        public ICommand CloseComment { get; }

        public CommentClothesSizeViewModel(
            ModalNavigationStore modalNavigationStore,
            ClothesStore clothesStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesSizeStore clothesSizeStore,
            SelectedClothesSizeStore selectedDetailedClothesItemStore,
            DVSListingViewModel dVSListingViewModel)
        {
            ICommand submitComment = new SubmitCommentClothesSizeCommand(
                this,
                clothesStore,
                categoryStore,
                seasonStore,
                clothesSizeStore,
                modalNavigationStore);

            CloseComment = new CloseCommentCommand(modalNavigationStore, dVSListingViewModel);

            CommentClothesSizeFormViewModel = new(submitComment, selectedDetailedClothesItemStore)
            {
                Comment = selectedDetailedClothesItemStore.SelectedClothesSize.Comment
            };
        }
    }
}
