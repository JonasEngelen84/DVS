namespace DVS.Domain.Models
{
    public class Size(string size)
    {
        public string Name { get; set; } = size;
        public int Quantity { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
    }
}
