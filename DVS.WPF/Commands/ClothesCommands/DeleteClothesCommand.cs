using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class DeleteClothesCommand(
        ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            clothesListingItemViewModel.HasError = false;

            EmployeeClothesSize? employeeClothesSize = employeeClothesSizeStore.EmployeeClothesSizes
                .FirstOrDefault(ecs => ecs.ClothesSize.ClothesId == clothesListingItemViewModel.Id);

            if (employeeClothesSize != null)
            {
                ShowErrorMessageBox("Die Bekleidung kann nicht gelöscht werden, da sie noch vergeben ist!", "Bekleidung löschen");
                return;
            }

            if (!Confirm($"Soll die Bekleidung  {clothesListingItemViewModel.Id}, {clothesListingItemViewModel.Name}  " +
                "wirklich gelöscht werden?" , "Bekleidung löschen"))
            {
                return;                
            }

            try
            {
                clothesListingItemViewModel.IsDeleting = true;
                await clothesStore.Delete(clothesListingItemViewModel.Clothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen der Bekleidung ist fehlgeschlagen!", "Bekleidung löschen");
                clothesListingItemViewModel.HasError = true;
            }
            finally
            {
                clothesListingItemViewModel.IsDeleting = false;
            }            
        }
    }
}
