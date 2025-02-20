using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
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
            AddEditClothesFormViewModel addClothesFormViewModel = addClothesViewModel.AddEditClothesFormViewModel;
            
            if (CheckClothesId(addClothesFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addClothesFormViewModel.HasError = false;
                addClothesFormViewModel.IsSubmitting = true;

                Clothes newClothes = CreateClothes(addClothesFormViewModel);
                List<Size> selectedSizes = GetSizes(addClothesFormViewModel);

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

        private Clothes CheckClothesId(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            Clothes? existingClothes = clothesStore.Clothes
                .FirstOrDefault(c => c.Id == addClothesFormViewModel.Id);

            return existingClothes;
        }

        private static Clothes CreateClothes(AddEditClothesFormViewModel addClothesFormViewModel)
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

        private static List<Size> GetSizes(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return new List<Size>(addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Any(size => size.IsSelected)
                    ? addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Where(size => size.IsSelected)
                    : addClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }

        private static void CreateClothesSizes(List<Size> selectedSizes, Clothes newClothes)
        {
            foreach (Size size in selectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size.Name, size.Quantity, "");
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

        private async Task AddClothesToDB(Clothes newClothes, AddEditClothesFormViewModel addClothesFormViewModel)
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
