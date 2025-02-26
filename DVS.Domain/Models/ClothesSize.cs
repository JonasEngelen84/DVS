using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize
    {
        public Guid GuidId { get; }
        public string ClothesId { get; private set; }
        public Clothes Clothes { get; private set; }
        public string Size { get; private set; }
        public int Quantity { get; private set; }
        public string Comment { get; private set; }

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        public ClothesSize(
            Guid guidId,
            Clothes clothes,
            string size,
            int quantity,
            string comment)
        {
            GuidId = guidId;
            Clothes = clothes;
            ClothesId = clothes.Id;
            Size = size;
            Quantity = quantity;
            Comment = comment;

            EmployeeClothesSizes = [];
        }

        public ClothesSize() {}
    }
}
