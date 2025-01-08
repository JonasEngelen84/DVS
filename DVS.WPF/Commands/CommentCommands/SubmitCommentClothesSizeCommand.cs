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
                                                 CategoryStore categoryStore,
                                                 SeasonStore seasonStore,
                                                 ClothesSizeStore clothesSizeStore,
                                                 ModalNavigationStore modalNavigationStore)
                                                 : AsyncCommandBase
    {
        private readonly CommentClothesSizeViewModel _commentClothesSizeViewModel = commentClothesSizeViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            CommentClothesSizeFormViewModel commentClothesSizeFormViewModel = _commentClothesSizeViewModel.CommentClothesSizeFormViewModel;
            commentClothesSizeFormViewModel.HasError = false;
            commentClothesSizeFormViewModel.IsSubmitting = true;

            ClothesSize clothesSizeToComment = GetClothesSizeToComment(commentClothesSizeFormViewModel);

            RemoveOldClothesSize(commentClothesSizeFormViewModel, clothesSizeToComment);

            ClothesSize editedClothesSize = CreateEditedClothesSizeInstance(commentClothesSizeFormViewModel, clothesSizeToComment);

            AddEditedClothesSizeToSizeAndClothesLists(commentClothesSizeFormViewModel, editedClothesSize);

            Clothes editedClothes = CreateEditedClothesSizeInstance(commentClothesSizeFormViewModel);

            AddEditedClothesToCategoryAndSeasonLists(commentClothesSizeFormViewModel, editedClothes);

            await UpdateClothesSizeDbAsync(commentClothesSizeFormViewModel, editedClothesSize);
            
            await UpdateClothesDbAsync(commentClothesSizeFormViewModel, editedClothes);
            
            await UpdateCategoryDbAsync(commentClothesSizeFormViewModel, editedClothes);
            
            await UpdateSeasonDbAsync(commentClothesSizeFormViewModel, editedClothes);

            _modalNavigationStore.Close();
        }

        private ClothesSize GetClothesSizeToComment(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel)
        {
            return commentClothesSizeFormViewModel.Clothes.Sizes
                .FirstOrDefault(s => s.Size.Size == commentClothesSizeFormViewModel.Size);
        }

        private void RemoveOldClothesSize(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, ClothesSize OldClothesSize)
        {
            commentClothesSizeFormViewModel.Clothes.Sizes.Remove(OldClothesSize);
            OldClothesSize.Size.ClothesSizes.Remove(OldClothesSize);
        }

        private ClothesSize CreateEditedClothesSizeInstance(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, ClothesSize clothesSizeToComment)
        {
            return new ClothesSize(clothesSizeToComment.GuidId,
                                   clothesSizeToComment.Clothes,
                                   clothesSizeToComment.Size,
                                   clothesSizeToComment.Quantity,
                                   commentClothesSizeFormViewModel.Comment)
            {
                EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(clothesSizeToComment.EmployeeClothesSizes)
            };
        }

        private void AddEditedClothesSizeToSizeAndClothesLists(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, ClothesSize editedClothesSize)
        {
            editedClothesSize.Size.ClothesSizes.Add(editedClothesSize);
            commentClothesSizeFormViewModel.Clothes.Sizes.Add(editedClothesSize);
        }

        private Clothes CreateEditedClothesSizeInstance(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel)
        {
            return new Clothes(commentClothesSizeFormViewModel.Clothes.GuidId,
                               commentClothesSizeFormViewModel.Clothes.Id,
                               commentClothesSizeFormViewModel.Clothes.Name,
                               commentClothesSizeFormViewModel.Clothes.Category,
                               commentClothesSizeFormViewModel.Clothes.Season,
                               commentClothesSizeFormViewModel.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(commentClothesSizeFormViewModel.Clothes.Sizes)
            };
        }

        private void AddEditedClothesToCategoryAndSeasonLists(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, Clothes editedClothes)
        {
            editedClothes.Category.Clothes.Remove(commentClothesSizeFormViewModel.Clothes);
            editedClothes.Category.Clothes.Add(editedClothes);
            editedClothes.Season.Clothes.Remove(commentClothesSizeFormViewModel.Clothes);
            editedClothes.Season.Clothes.Add(editedClothes);
        }

        private async Task UpdateClothesSizeDbAsync(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, ClothesSize editedClothesSize)
        {
            try
            {
                await _clothesSizeStore.Update(editedClothesSize);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidungsgröße Kommentieren");

                commentClothesSizeFormViewModel.HasError = true;
            }
            finally
            {
                commentClothesSizeFormViewModel.IsSubmitting = false;
            }
        }
        
        private async Task UpdateClothesDbAsync(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, Clothes editedClothes)
        {
            try
            {
                await _clothesStore.Update(editedClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidungsgröße Kommentieren");

                commentClothesSizeFormViewModel.HasError = true;
            }
            finally
            {
                commentClothesSizeFormViewModel.IsSubmitting = false;
            }
        }
        
        private async Task UpdateCategoryDbAsync(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, Clothes editedClothes)
        {
            try
            {
                await _categoryStore.Update(editedClothes.Category, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidungsgröße Kommentieren");

                commentClothesSizeFormViewModel.HasError = true;
            }
            finally
            {
                commentClothesSizeFormViewModel.IsSubmitting = false;
            }
        }
        
        private async Task UpdateSeasonDbAsync(CommentClothesSizeFormViewModel commentClothesSizeFormViewModel, Clothes editedClothes)
        {
            try
            {
                await _seasonStore.Update(editedClothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Kommentieren der Bekleidungsgröße ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidungsgröße Kommentieren");

                commentClothesSizeFormViewModel.HasError = true;
            }
            finally
            {
                commentClothesSizeFormViewModel.IsSubmitting = false;
            }
        }
    }
}
