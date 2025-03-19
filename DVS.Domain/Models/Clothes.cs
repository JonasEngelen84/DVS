using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Clothes : ObservableEntity
    {
        public string Id { get; set; }
        public Guid SeasonGuidId { get; set; }
        public Guid CategoryGuidId { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                }
            }
        }

        private Season _season;
        public Season Season
        {
            get => _season;
            set
            {
                if (_season != value)
                {
                    _season = value;
                }
            }
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                }
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                }
            }
        }

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
            SeasonGuidId = season.Id;
            CategoryGuidId = category.Id;
            Id = id;
            Name = name;
            Comment = comment;

            Sizes = [];
        }

        public Clothes() { }
    }
}
