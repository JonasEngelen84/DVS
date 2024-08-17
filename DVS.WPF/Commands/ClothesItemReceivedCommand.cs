using DVS.Domain.Models;
using DVS.WPF.ViewModels;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands
{
    public class ClothesItemReceivedCommand(DetailedClothesListingItemViewModel selectedDetailedClothesItem,
                                            Action<Clothes> addItemToAvailableSizes,
                                            Action<Clothes> addItemToEmployeeClothesList)
                                            : AsyncCommandBase
    {
        private readonly DetailedClothesListingItemViewModel _selectedDetailedClothesItem = selectedDetailedClothesItem;

        public readonly Action<Clothes> _addItemToAvailableSizes = addItemToAvailableSizes;
        public readonly Action<Clothes> _addItemToEmployeeClothesList = addItemToEmployeeClothesList;

        public override async Task ExecuteAsync(object parameter)
        {
            if (_selectedDetailedClothesItem == null || _selectedDetailedClothesItem.Quantity == 0)
            {
                return;
            }

            // Menge erhöhen der verschobenen Kleidungsgröße
            ClothesSize editedItem = new(_selectedDetailedClothesItem.ClothesSize.GuidID,
                                         _selectedDetailedClothesItem.Clothes,
                                         _selectedDetailedClothesItem.ClothesSize.Size,
                                         _selectedDetailedClothesItem.ClothesSize.Quantity + 1,
                                         _selectedDetailedClothesItem.ClothesSize.Comment);

            // Geänderte Clothes-Instanz erstellen und übergeben für späteres update in DB und ClothesStore
            Clothes editedClothes = new(_selectedDetailedClothesItem.Clothes.GuidID,
                                        _selectedDetailedClothesItem.Clothes.ID,
                                        _selectedDetailedClothesItem.Clothes.Name,
                                        _selectedDetailedClothesItem.Clothes.Category,
                                        _selectedDetailedClothesItem.Clothes.Season,
                                        _selectedDetailedClothesItem.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(_selectedDetailedClothesItem.Clothes.Sizes
                    .Select(s => new ClothesSize(s.GuidID, s.Clothes, s.Size, s.Quantity, s.Comment)))
            };

            // Alte Bekleidungsgrößen entfernen und neue hinzufügen
            ClothesSize itemToRemove = editedClothes.Sizes.FirstOrDefault(cs => cs.GuidID == editedItem.GuidID);
            editedClothes.Sizes.Remove(itemToRemove);
            editedClothes.Sizes.Add(editedItem);

            if (parameter.Equals("EmployeeClothesList"))
            {
                _addItemToEmployeeClothesList?.Invoke(editedClothes);
            }
            else
            {                        
                _addItemToAvailableSizes?.Invoke(editedClothes);
            }
        }
    }
}
