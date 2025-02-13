using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class AddClothesCommand(AddClothesViewModel addClothesViewModel,
                                   ClothesStore clothesStore,
                                   ClothesSizeStore clothesSizeStore,
                                   ModalNavigationStore modalNavigationStore)
                                   : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddEditClothesFormViewModel;
            
            if (CheckClothesId(addClothesFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addClothesFormViewModel.HasError = false;
                addClothesFormViewModel.IsSubmitting = true;

                Clothes newClothes = CreateClothes(addClothesFormViewModel);
                List<SizeModel> selectedSizes = GetSizes(addClothesFormViewModel);

                if (selectedSizes != null)
                {
                    CreateClothesSizes(selectedSizes, newClothes);
                    AddClothesSizeToStore(newClothes, clothesSizeStore);
                }
                
                await AddClothesToDB(newClothes, addClothesFormViewModel);

                addClothesFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }            
        }

        private Clothes CheckClothesId(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            Clothes? existingClothes = _clothesStore.Clothes
                .FirstOrDefault(c => c.Id == addClothesFormViewModel.Id);

            return existingClothes;
        }

        private static Clothes CreateClothes(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return new Clothes(addClothesFormViewModel.Id,
                               addClothesFormViewModel.Name,
                               addClothesFormViewModel.Category,
                               addClothesFormViewModel.Season,
                               addClothesFormViewModel.Comment)
            {
                Sizes = []
            };
        }

        private static List<SizeModel> GetSizes(AddEditClothesFormViewModel addClothesFormViewModel)
        {
            return new List<SizeModel>(addClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? addClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : addClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }

        private static void CreateClothesSizes(List<SizeModel> selectedSizes, Clothes newClothes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size, size.Quantity, "");
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
                await _clothesStore.Add(newClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!", "AddClothesCommand CreateAndAddNewClothesAsync");

                addClothesFormViewModel.HasError = true;
            }
        }
    }
}
