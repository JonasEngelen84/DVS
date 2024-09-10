using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes(Guid guidID, string id, string name, Category category, Season season, string comment)
    {
        public Guid GuidID { get; } = guidID;
        public Guid CategoryGuidID { get; } = category.GuidID;
        public Guid SeasonGuidID { get; } = season.GuidID;
        public Category Category { get; } = category;
        public Season Season { get; } = season;
        public string ID { get; } = id;
        public string Name { get; } = name;
        public string Comment { get; } = comment;

        public ObservableCollection<ClothesSize> Sizes { get; set; } = [];
    }
}
