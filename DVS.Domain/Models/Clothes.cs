using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes
    {
        public Guid GuidID { get; }
        public Guid CategoryGuidID { get; private set; }
        public Guid SeasonGuidID { get; private set; }
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public Category Category { get; private set; }
        public Season Season { get; private set; }

        public ObservableCollection<ClothesSize> Sizes { get; set; }

        public Clothes(Guid guidID, string id, string name, Category category, Season season, string comment)
        {
            GuidID = guidID;
            ID = id;
            Name = name;
            CategoryGuidID = category.GuidID;
            SeasonGuidID = season.GuidID;
            Category = category;
            Season = season;
            Comment = comment;

            Sizes = [];
        }

        public Clothes()
        {

        }
    }
}
