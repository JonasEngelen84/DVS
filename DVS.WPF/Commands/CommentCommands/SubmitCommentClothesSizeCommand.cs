using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace DVS.WPF.Commands.CommentCommands
{
    public class SubmitCommentClothesSizeCommand(CommentClothesSizeViewModel commentClothesSizeViewModel,
                                                 ClothesStore clothesStore,
                                                 ModalNavigationStore modalNavigationStore)
                                                 : AsyncCommandBase
    {
        private readonly CommentClothesSizeViewModel _commentClothesSizeViewModel = commentClothesSizeViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            CommentClothesSizeFormViewModel commentClothesSizeFormViewModel = _commentClothesSizeViewModel.CommentClothesSizeFormViewModel;

            commentClothesSizeFormViewModel.IsSubmitting = true;

            ClothesSize existingItem = commentClothesSizeFormViewModel.Clothes.Sizes
                .FirstOrDefault(s => s.Size.Size == commentClothesSizeFormViewModel.Size);

            existingItem.Comment = commentClothesSizeFormViewModel.Comment;

            Clothes updatedClothes = new(commentClothesSizeFormViewModel.Clothes.GuidID,
                                         commentClothesSizeFormViewModel.ID,
                                         commentClothesSizeFormViewModel.Name,
                                         commentClothesSizeFormViewModel.Clothes.Category,
                                         commentClothesSizeFormViewModel.Clothes.Season,
                                         commentClothesSizeFormViewModel.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(commentClothesSizeFormViewModel.Clothes.Sizes
                .Select(s => new ClothesSize(s.Clothes, s.Size, s.Quantity) { Comment = s.Comment }))
            };
                        
            try
            {
                await _clothesStore.Update(updatedClothes);
            }
            catch (Exception)
            {
                string messageBoxText = "Kommentieren der Bekleidungsgröße ist fehlgeschlagen";
                string caption = "Bekleidungsgröße Kommentieren";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);
            }
            finally
            {
                commentClothesSizeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
