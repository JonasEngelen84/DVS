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
            
            if (CheckClothesId(addClothesFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addClothesFormViewModel.HasError = false;
                addClothesFormViewModel.IsSubmitting = true;

                Clothes newClothes = CreateClothes(addClothesFormViewModel);
                List<SizeListingItemViewModel> selectedSizes = GetSizes(addClothesFormViewModel);

                if (selectedSizes != null)
                {
                    CreateClothesSizes(selectedSizes, newClothes);
                    AddClothesSizeToStore(newClothes, clothesSizeStore);
                }
                
                await AddClothesToDB(newClothes, addClothesFormViewModel);

                addClothesFormViewModel.IsSubmitting = false;
                modalNavigationStore.Close();
            }
        }

        private Clothes CheckClothesId(AddClothesFormViewModel addClothesFormViewModel)
        {
            Clothes? existingClothes = clothesStore.Clothes
                .FirstOrDefault(c => c.Id == addClothesFormViewModel.Id);

            return existingClothes;
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

        private static List<SizeListingItemViewModel> GetSizes(AddClothesFormViewModel addClothesFormViewModel)
        {
            return new List<SizeListingItemViewModel>(addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Any(size => size.Quantity > 0)
                    ? addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Where(size => size.Quantity > 0)
                    : addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesEU.Where(size => size.Quantity > 0))
                    .ToList();
        }

        private static void CreateClothesSizes(List<SizeListingItemViewModel> selectedSizes, Clothes newClothes)
        {
            foreach (SizeListingItemViewModel size in selectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size.Size, size.Quantity, size.Comment);
                newClothes.Sizes.Add(newClothesSize);                
            }
        }

        private static void AddClothesSizeToStore(Clothes newClothes, ClothesSizeStore clothesSizeStore)
        {
            foreach (ClothesSize cs in newClothes.Sizes)
            {
                clothesSizeStore.AddToStore(cs);
            }
        }

        private async Task AddClothesToDB(Clothes newClothes, AddClothesFormViewModel addClothesFormViewModel)
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
