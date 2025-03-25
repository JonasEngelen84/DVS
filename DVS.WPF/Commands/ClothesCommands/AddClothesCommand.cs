using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class AddClothesCommand(
        AddClothesViewModel addClothesViewModel,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddClothesFormViewModel addClothesFormViewModel = addClothesViewModel.AddClothesFormViewModel;
            addClothesFormViewModel.HasError = false;
            
            if (clothesStore.Clothes.Any(c => c.Id == addClothesFormViewModel.Id))
            {
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
                return;
            }

            addClothesFormViewModel.IsSubmitting = true;

            Clothes newClothes = CreateClothes(addClothesFormViewModel);
            List<SizeListingItemViewModel> selectedSizes = GetSelectedSizes(addClothesFormViewModel);

            if (selectedSizes != null)
            {
                CreateClothesSizes(selectedSizes, newClothes);
            }

            await AddClothes(newClothes, addClothesFormViewModel);

            addClothesFormViewModel.IsSubmitting = false;
            modalNavigationStore.Close();
        }

        private static Clothes CreateClothes(AddClothesFormViewModel addClothesFormViewModel)
        {
            return new Clothes(
                addClothesFormViewModel.Id,
                addClothesFormViewModel.Name,
                addClothesFormViewModel.Category,
                addClothesFormViewModel.Season,
                addClothesFormViewModel.Comment)
            {
                Sizes = []
            };
        }
        private static List<SizeListingItemViewModel> GetSelectedSizes(AddClothesFormViewModel addClothesFormViewModel)
        {
            return new List<SizeListingItemViewModel>(addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS
                .Any(size => size.IsChecked)
                ? addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Where(size => size.IsChecked)
                : addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesEU.Where(size => size.IsChecked))
                .ToList();
        }
        private void CreateClothesSizes(List<SizeListingItemViewModel> selectedSizes, Clothes newClothes)
        {
            foreach (SizeListingItemViewModel size in selectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size.Size, size.Quantity, size.Comment)
                {
                    EmployeeClothesSizes = []
                };

                newClothes.Sizes.Add(newClothesSize);
                clothesSizeStore.AddStore(newClothesSize);
            }
        }
        private async Task AddClothes(Clothes newClothes, AddClothesFormViewModel addClothesFormViewModel)
        {
            try
            {
                await clothesStore.Add(newClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!", "AddClothesCommand CreateAndAddNewClothesAsync");

                addClothesFormViewModel.HasError = true;
            }
        }
    }
}
