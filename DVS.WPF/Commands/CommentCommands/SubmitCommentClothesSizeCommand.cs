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
            commentClothesSizeFormViewModel.HasError = false;
            commentClothesSizeFormViewModel.IsSubmitting = true;

            // Zu kommentierende ClothesSize speichern
            ClothesSize clothesSizeToComment = commentClothesSizeFormViewModel.Clothes.Sizes
                .FirstOrDefault(s => s.Size.Size == commentClothesSizeFormViewModel.Size);
            
            // Neues, kommentiertes, ClothesSize erstellen
            ClothesSize commentedClothesSize = new(clothesSizeToComment.GuidID,
                                                   clothesSizeToComment.Clothes,
                                                   clothesSizeToComment.Size,
                                                   clothesSizeToComment.Quantity,
                                                   commentClothesSizeFormViewModel.Comment);

            // Sämtliche alten ClothesSizes aus SizeModel-Liste und DB entfernen
            foreach (ClothesSize cs in commentClothesSizeFormViewModel.Clothes.Sizes)
            {
                cs.Size.ClothesSizes.Remove(cs);

                try
                {
                    await _clothesStore.DeleteClothesSize(cs.GuidID);
                }
                catch (Exception)
                {
                    string messageBoxText = $"Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    string caption = " Bekleidungsgröße Kommentieren";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    commentClothesSizeFormViewModel.HasError = true;
                }
            }

            // Altes ClothesSize entfernen und neues einfügen
            commentClothesSizeFormViewModel.Clothes.Sizes.Remove(clothesSizeToComment);
            commentClothesSizeFormViewModel.Clothes.Sizes.Add(commentedClothesSize);

            // Neues Clothes mit neuer Größen-Liste erstellen
            Clothes updatedClothes = new(commentClothesSizeFormViewModel.Clothes.GuidID,
                                         commentClothesSizeFormViewModel.ID,
                                         commentClothesSizeFormViewModel.Name,
                                         commentClothesSizeFormViewModel.Clothes.Category,
                                         commentClothesSizeFormViewModel.Clothes.Season,
                                         commentClothesSizeFormViewModel.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(commentClothesSizeFormViewModel.Clothes.Sizes
                .Select(s => new ClothesSize(s.GuidID, s.Clothes, s.Size, s.Quantity, s.Comment)))
            };

            // ClothesSizes den Size-Listen hinzufügen
            foreach (ClothesSize size in updatedClothes.Sizes)
            {
                size.Size.ClothesSizes.Add(size);
            }

            // Neue Clothes-Instanz den Listen von category und saison hinzufügen
            updatedClothes.Category?.Clothes.Add(updatedClothes);
            updatedClothes.Season?.Clothes.Add(updatedClothes);

            try
            {
                await _clothesStore.Update(updatedClothes);
            }
            catch (Exception)
            {
                string messageBoxText = "Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                string caption = "Bekleidungsgröße Kommentieren";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                commentClothesSizeFormViewModel.HasError = true;
            }
            finally
            {
                commentClothesSizeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
