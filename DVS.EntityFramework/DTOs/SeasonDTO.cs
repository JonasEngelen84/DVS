using System.ComponentModel.DataAnnotations;

namespace DVS.EntityFramework.DTOs
{
    public class SeasonDTO
    {
        public Guid? GuidID { get; set; }
        public string Name { get; set; }
    }
}
