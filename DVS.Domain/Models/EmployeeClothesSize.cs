namespace DVS.Domain.Models
{
    public class EmployeeClothesSize(Guid guidID, Employee employee, ClothesSize clothesSize, int quantity, string? comment)
    {
        public Guid GuidID { get; } = guidID;
        public Guid EmployeeGuidID { get; } = employee.GuidID;
        public Guid ClothesSizeGuidID { get; } = clothesSize.GuidID;
        public Employee Employee { get; } = employee;
        public ClothesSize ClothesSize { get; } = clothesSize;
        public int Quantity { get; } = quantity;
        public string? Comment { get; } = comment;
    }
}
