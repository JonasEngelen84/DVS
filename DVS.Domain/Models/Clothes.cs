using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes
    {
        public Guid GuidId { get; }
        public Guid SeasonGuidId { get; private set; }
        public Guid CategoryGuidId { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public Season Season { get; private set; }
        public Category Category { get; private set; }

        public ObservableCollection<ClothesSize> Sizes { get; set; }

        public Clothes(Guid guidId, string id, string name, Category category, Season season, string comment)
        {
            GuidId = guidId;
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
