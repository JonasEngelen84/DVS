using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes
    {
        public Guid SeasonGuidId { get; }
        public Guid CategoryGuidId { get; }
        public string Id { get; }
        public string Name { get; }
        public string Comment { get; }
        public Season Season { get; }
        public Category Category { get; }

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
