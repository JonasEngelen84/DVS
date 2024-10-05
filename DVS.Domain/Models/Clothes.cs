using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes
    {
        public Guid GuidID { get; }
        public Guid SeasonGuidID { get; private set; }
        public Guid CategoryGuidID { get; private set; }
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public Season Season { get; private set; }
        public Category Category { get; private set; }

        public ObservableCollection<ClothesSize> Sizes { get; set; }

        public Clothes(Guid guidID, string id, string name, Category category, Season season, string comment)
        {
            GuidID = guidID;
            Season = season;
            Category = category;
            SeasonGuidID = season.GuidID;
            CategoryGuidID = category.GuidID;
            ID = id;
            Name = name;
            Comment = comment;

            Sizes = [];
        }

        public Clothes()
        {

        }
    }
}
