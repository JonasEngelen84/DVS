namespace DVS.EntityFramework.DTOs
{
    public class EmployeeClothesSizeDTO
    {
        public Guid GuidID { get; set; }
        public Guid EmployeeGuidID { get; set; }
        public Guid ClothesSizeGuidID { get; set; }
        public int Quantity { get; set; }
        public string? Comment { get; set; }
    }
}
