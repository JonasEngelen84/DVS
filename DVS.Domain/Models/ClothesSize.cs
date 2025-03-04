using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize
    {
        public Guid GuidId { get; }
        public string ClothesId { get; }
        public Clothes Clothes { get; }
        public string Size { get; }
        public int Quantity { get; }
        public string Comment { get; }

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
