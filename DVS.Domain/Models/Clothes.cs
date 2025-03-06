using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes : ObservableEntity
    {
        public Guid SeasonGuidId { get; set; }
        public Guid CategoryGuidId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Season Season { get; set; }
        public Category Category { get; set; }

        public ObservableCollection<ClothesSize> Sizes { get; set; }

        public Clothes(
            string id,
            string name,
            Category category,
            Season season,
            string comment)
        {
            Season = season;
            Category = category;
            SeasonGuidId = season.GuidId;
            CategoryGuidId = category.GuidId;
            Id = id;
            Name = name;
            Comment = comment;

            Sizes = [];
        }

        public Clothes() {}
    }
}
