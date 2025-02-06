using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class AddClothesCommand(AddClothesViewModel addClothesViewModel,
                                   ClothesStore clothesStore,
                                   ModalNavigationStore modalNavigationStore)
                                   : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddEditClothesFormViewModel;

            Clothes? existingClothes = _clothesStore.Clothes
                .FirstOrDefault(c => c.Id == addClothesFormViewModel.Id);

            if (existingClothes != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addClothesFormViewModel.HasError = false;
                addClothesFormViewModel.IsSubmitting = true;

                Clothes newClothes = new(Guid.NewGuid(),
                                         addClothesFormViewModel.Id,
                                         addClothesFormViewModel.Name,
                                         addClothesFormViewModel.Category,
                                         addClothesFormViewModel.Season,
                                         addClothesFormViewModel.Comment);

                // Die vom User gewählten Größen/SizeModel auflisten
                List<SizeModel> selectedSizes = (addClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? addClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : addClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();

                if (selectedSizes != null)
                {
                    foreach (SizeModel size in selectedSizes)
                    {
                        ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size, size.Quantity, "");
                        newClothes.Sizes.Add(newClothesSize);
                    }
                }

                try
                {
                    await _clothesStore.Add(newClothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!", "AddClothesCommand CreateAndAddNewClothesAsync");

                    addClothesFormViewModel.HasError = true;
                }

                addClothesFormViewModel.IsSubmitting = false;

                _modalNavigationStore.Close();
            }            
        }
    }
}
