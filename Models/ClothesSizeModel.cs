namespace DVS.Models
{
    public class ClothesSizeModel(string size, int quantity, string? comment)
    {
        public string Size { get; set; } = size;
        public int Quantity { get; set; } = quantity;
        public string? Comment { get; set; } = comment;
    }
}
