using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Category : ObservableEntity
    { 
        public Guid Id { get; set; }

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

        public ObservableCollection<Clothes> Clothes { get; set; }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;

            Clothes = [];
        }

        public Category() { }
    }
}
