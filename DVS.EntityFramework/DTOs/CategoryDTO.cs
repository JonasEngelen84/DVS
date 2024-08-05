using System.ComponentModel.DataAnnotations;

namespace DVS.EntityFramework.DTOs
{
    public class CategoryDTO
    {
        public Guid? GuidID { get; set; }
        public string Name { get; set; }
    }
}
