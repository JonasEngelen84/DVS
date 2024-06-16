using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.ClothesCommands
{
    public class AddClothesCommand(AddClothesViewModel addClothesViewModel,
                                   ClothesStore clothesStore)
                                   : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddClothesFormViewModel;

            addClothesFormViewModel.ErrorMessage = null;
            addClothesFormViewModel.IsSubmitting = true;

            var selectedSizes = addClothesFormViewModel.AvailableSizesUS.Any(size => size.IsSelected)
            ? addClothesFormViewModel.AvailableSizesUS.Where(size => size.IsSelected)
            : addClothesFormViewModel.AvailableSizesEU.Where(size => size.IsSelected);

            ClothesModel clothes = new(addClothesFormViewModel.ID,
                                       addClothesFormViewModel.Name,
                                       addClothesFormViewModel.Category,
                                       addClothesFormViewModel.Season,
                                       addClothesFormViewModel.Comment);

            foreach (var size in selectedSizes)
            {
                clothes.Sizes.Add(new ClothesSizeModel(size.Size, size.Quantity, size.Comment));
            }

            try
            {
                await _clothesStore.Add(clothes);
            }
            catch (Exception)
            {
                addClothesFormViewModel.ErrorMessage = "Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addClothesFormViewModel.IsSubmitting = false;
            }
        }
    }
}
