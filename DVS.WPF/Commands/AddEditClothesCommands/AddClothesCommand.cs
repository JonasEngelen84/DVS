using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class AddClothesCommand(AddClothesViewModel addClothesViewModel,
                                   ClothesStore clothesStore,
                                   SizeStore sizeStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesSizeStore clothesSizeStore,
                                   ModalNavigationStore modalNavigationStore)
                                   : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddEditClothesFormViewModel;
            addClothesFormViewModel.HasError = false;
            addClothesFormViewModel.IsSubmitting = true;

            var selectedSizes = GetSelectedSizes(addClothesFormViewModel);
            Clothes newClothes = CreateNewClothesInstance(addClothesFormViewModel);
            CreateAndAddClothesSizesAsync(addClothesFormViewModel, selectedSizes, newClothes);
            UpdateSizeAsync(addClothesFormViewModel, selectedSizes);
            UpdateCategoryAndSeasonCollectionsAsync(addClothesFormViewModel, newClothes);
            UpdateClothesAsync(addClothesFormViewModel, newClothes);

            addClothesFormViewModel.IsSubmitting = false;

            _modalNavigationStore.Close();
        }

        private static List<SizeModel> GetSelectedSizes(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return (addClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? addClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : addClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }

        private static Clothes CreateNewClothesInstance(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return new Clothes(Guid.NewGuid(),
                               addClothesFormViewModel.ID,
                               addClothesFormViewModel.Name,
                               addClothesFormViewModel.Category,
                               addClothesFormViewModel.Season,
                               addClothesFormViewModel.Comment);
        }

        private async Task CreateAndAddClothesSizesAsync(AddEditClothesFormViewModel addClothesFormViewModel, List<SizeModel> selectedSizes, Clothes newClothes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size, size.Quantity, null);

                newClothes.Sizes.Add(newClothesSize);
                size.ClothesSizes.Add(newClothesSize);

                try
                {
                    await _clothesSizeStore.Update(newClothesSize);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "AddClothesCommand CreateAndAddClothesSizesAsync");

                    addClothesFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateSizeAsync(AddEditClothesFormViewModel addClothesFormViewModel, List<SizeModel> selectedSizes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                try
                {
                    await _sizeStore.Update(size);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "AddClothesCommand UpdateSizeAsync");

                    addClothesFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateCategoryAndSeasonCollectionsAsync(AddEditClothesFormViewModel addClothesFormViewModel, Clothes newClothes)
        {
            newClothes.Category?.Clothes.Add(newClothes);
            newClothes.Season?.Clothes.Add(newClothes);

            try
            {
                await _categoryStore.Update(newClothes.Category, null);
                await _seasonStore.Update(newClothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "AddClothesCommand UpdateCategoryAndSeasonCollectionsAsync");

                addClothesFormViewModel.HasError = true;
            }
        }

        private async Task UpdateClothesAsync(AddEditClothesFormViewModel addClothesFormViewModel, Clothes newClothes)
        {
            try
            {
                await _clothesStore.Add(newClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "AddClothesCommand UpdateClothesAsync");

                addClothesFormViewModel.HasError = true;
            }
        }
    }
}
