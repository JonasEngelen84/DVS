namespace DVS.Models
{
    public class ClothesSizeModel(string size, int quantity)
    {
        private string _size { get; set; } = size;
        private int _quantity { get; set; } = quantity;
    }
}
