using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes(Guid guidID, string id, string name, Category category, Season season, string? comment)
    {
        public Guid GuidID { get; } = guidID;
        public string ID { get; } = id;
        public string Name { get; } = name;
        public Category Category { get; } = category;
        public Season Season { get; } = season;
        public Guid CategoryGuidID = (Guid)category.GuidID;
        public Guid SeasonGuidID = (Guid)season.GuidID;
        public string? Comment { get; } = comment;

        
        public ObservableCollection<ClothesSize> Sizes { get; set; } = [];
    }
}
