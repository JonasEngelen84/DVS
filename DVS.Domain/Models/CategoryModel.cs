namespace DVS.Domain.Models
{
    public class CategoryModel(Guid? guidID, string name)
    {
        public Guid? GuidID { get; set; } = guidID;
        public string Name { get; set; } = name;
    }
}
