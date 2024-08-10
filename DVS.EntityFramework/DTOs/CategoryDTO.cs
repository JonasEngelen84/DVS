using DVS.Domain.Models;
using System.Collections.ObjectModel;

namespace DVS.EntityFramework.DTOs
{
    public class CategoryDTO
    {
        public Guid GuidID { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Clothes> Clothes { get; set; } = [];
    }
}
