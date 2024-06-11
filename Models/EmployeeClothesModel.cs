namespace DVS.Models
{
    public class EmployeeClothesModel(ClothesModel clothes, int quantity)
    {
        private ClothesModel _clothes { get; set; } = clothes;
        private int _quantity { get; set; } = quantity;
    }
}
