using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CommentCommands
{
    public class SubmitCommentClothesSizeCommand(CommentClothesSizeViewModel commentClothesSizeViewModel,
        ClothesStore clothesStore, ModalNavigationStore modalNavigationStore) : AsyncCommandBase
    {
        private readonly CommentClothesSizeViewModel _commentClothesSizeViewModel = commentClothesSizeViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            CommentClothesSizeFormViewModel commentClothesSizeFormViewModel = _commentClothesSizeViewModel.CommentClothesSizeFormViewModel;

            commentClothesSizeFormViewModel.ErrorMessage = null;
            commentClothesSizeFormViewModel.IsSubmitting = true;

            Clothes clothesToEdit = new(commentClothesSizeFormViewModel.Clothes.GuidID,
                                        commentClothesSizeFormViewModel.ID,
                                        commentClothesSizeFormViewModel.Name,
                                        commentClothesSizeFormViewModel.Clothes.Category,
                                        commentClothesSizeFormViewModel.Clothes.Season,
                                        commentClothesSizeFormViewModel.Clothes.Comment)
            {
                Sizes = commentClothesSizeFormViewModel.Clothes.Sizes
            };

            ClothesSize existingItem = clothesToEdit.Sizes.FirstOrDefault(s => s.Size.Size == commentClothesSizeFormViewModel.Size);

            if (existingItem == null)
            {
                commentClothesSizeFormViewModel.ErrorMessage =
                    "Bearbeiten des Kommentar ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            else
            {
                existingItem.Comment = commentClothesSizeFormViewModel.Comment;

                try
                {
                    await _clothesStore.Update(clothesToEdit);
                }
                catch (Exception)
                {
                    commentClothesSizeFormViewModel.ErrorMessage =
                    "Bearbeiten des Kommentar ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    commentClothesSizeFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
