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
                                   EmployeeClothesSizesStore employeeClothesSizesStore,
                                   EmployeeStore employeeStore,
                                   ModalNavigationStore modalNavigationStore)
                                   : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddEditClothesFormViewModel;
            addClothesFormViewModel.HasError = false;
            addClothesFormViewModel.IsSubmitting = true;

            var selectedSizes = GetSelectedSizes(addClothesFormViewModel);

            Clothes newClothes = CreateNewClothesInstance(addClothesFormViewModel);

            AddClothesSizesAsync(addClothesFormViewModel, selectedSizes, newClothes);

            UpdateCategoryAndSeasonCollectionsAsync(addClothesFormViewModel, newClothes);

            UpdateClothesAsync(addClothesFormViewModel, newClothes);

            addClothesFormViewModel.IsSubmitting = false;

            _modalNavigationStore.Close();
        }

        private List<SizeModel> GetSelectedSizes(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return (addClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? addClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : addClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }

        private Clothes CreateNewClothesInstance(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return new Clothes(Guid.NewGuid(),
                               addClothesFormViewModel.ID,
                               addClothesFormViewModel.Name,
                               addClothesFormViewModel.Category,
                               addClothesFormViewModel.Season,
                               addClothesFormViewModel.Comment);
        }

        private async Task AddClothesSizesAsync(AddEditClothesFormViewModel addClothesFormViewModel, List<SizeModel> selectedSizes, Clothes newClothes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize clothesSize = new(Guid.NewGuid(), newClothes, size, size.Quantity, null);

                newClothes.Sizes.Add(clothesSize);
                size.ClothesSizes.Add(clothesSize);

                try
                {
                    await _sizeStore.Update(size);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung erstellen");

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
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung erstellen");

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
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung erstellen");

                addClothesFormViewModel.HasError = true;
            }
        }
    }
}
