using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class ClearSizesCommand(
        ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore,
        EmployeeStore employeeStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : AsyncCommandBase
    {
        private readonly List<ClothesSize> ClothesSizesToEdit = [];
        private readonly List<EmployeeClothesSize> assignedClothesSizes = [];

        public override async Task ExecuteAsync(object parameter)
        {
            if (Confirm($"Alle Größen der Bekleidung  \"{clothesListingItemViewModel.Id}, {clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                "\n\nLöschen fortsetzen?", "Alle Bekleidungs-Größen löschen"))
            {
                clothesListingItemViewModel.IsDeleting = true;
                clothesListingItemViewModel.HasError = false;

                Clothes editedClothes = CreateEditedClothes();
                await DeleteClothesSizes(editedClothes);
                
                if (ClothesSizesToEdit.Count > 0)
                {
                    await UpdateClothesSizes(editedClothes);
                }

                await UpdateClothes(editedClothes);

                clothesListingItemViewModel.IsDeleting = false;
            }
        }

        private Clothes CreateEditedClothes()
        {
            Clothes editedClothes = new(clothesListingItemViewModel.Id,
                                        clothesListingItemViewModel.Name,
                                        clothesListingItemViewModel.Category,
                                        clothesListingItemViewModel.Season,
                                        clothesListingItemViewModel.Comment)
            {
                Sizes = []
            };

            return editedClothes;
        }

        private async Task DeleteClothesSizes(Clothes editedClothes)
        {
            foreach (ClothesSize clothesSize in clothesListingItemViewModel.Sizes)
            {
                EmployeeClothesSize? assignedClothesSize = employeeClothesSizeStore.EmployeeClothesSizes
                    .FirstOrDefault(ecs => ecs.ClothesSizeGuidId == clothesSize.GuidId);

                if (assignedClothesSize != null)
                {
                    ClothesSizesToEdit.Add(clothesSize);
                    ShowErrorMessageBox($"Löschen der Größe  {clothesSize.Size}  ist nicht möglich, da diese Größe in Besitz ist!", "Bekleidungs-Größen löschen");
                }
                else
                {
                    try
                    {
                        await clothesSizeStore.Delete(clothesSize);

                        ClothesSize existingClothesSize = clothesListingItemViewModel.Sizes
                            .First(cs => cs.GuidId == clothesSize.GuidId);

                        editedClothes.Sizes.Remove(existingClothesSize);
                    }
                    catch
                    {
                        ShowErrorMessageBox($"Löschen der Größe  {clothesSize.Size}  ist fehlgeschlagen!", "Bekleidungs-Größen löschen");
                    }
                }
            }
        }

        private async Task UpdateClothesSizes(Clothes editedClothes)
        {            
            foreach (ClothesSize clothesSize in ClothesSizesToEdit)
            {
                ClothesSize editedClothesSize = new(
                    clothesSize.GuidId,
                    editedClothes,
                    clothesSize.Size,
                    clothesSize.Quantity,
                    clothesSize.Comment)
                {
                    EmployeeClothesSizes = clothesSize.EmployeeClothesSizes
                };

                try
                {
                    await clothesSizeStore.Update(editedClothesSize);
                }
                catch
                {
                    ShowErrorMessageBox("Löschen der Bekleidungs-Größen ist fehlgeschlagen!", "Bekleidungs-Größen löschen");
                }

                editedClothes.Sizes.Add(editedClothesSize);
            }
        }

        private async Task UpdateClothes(Clothes editedClothes)
        {
            try
            {
                await clothesStore.Update(editedClothes);
            }
            catch
            {
                ShowErrorMessageBox("Löschen der Bekleidungs-Größen ist fehlgeschlagen!", "Bekleidungs-Größen löschen");
            }
        }

        private async Task UpdateEmployeeClothesSizes(Clothes editedClothes)
        {
            foreach (ClothesSize clothesSize in  editedClothes.Sizes)
            {
                assignedClothesSizes = employeeClothesSizeStore.EmployeeClothesSizes
                .Where(ecs => ecs.ClothesSizeGuidId == clothesSize.GuidId)
                .ToList();

                foreach (EmployeeClothesSize ecs in assignedClothesSizes)
                {
                    EmployeeClothesSize editedEcs = new(ecs.Employee, cl)
                }
            }
        }
    }
}
