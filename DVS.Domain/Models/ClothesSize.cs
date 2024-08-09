using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize(Clothes clothes, SizeModel size, int quantity)
    {
        public Guid GuidID = Guid.NewGuid();
        public Clothes Clothes { get; } = clothes;
        public SizeModel Size { get; } = size;
        public Guid ClothesGuidID { get; } = clothes.GuidID;
        public Guid SizeGuidId { get; } = size.GuidID;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; }


        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; } = [];
    }
}
