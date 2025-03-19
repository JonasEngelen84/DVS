using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Season : ObservableEntity
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

        public Season(Guid id, string name)
        {
            Id = id;
            Name = name;

            Clothes = [];
        }

        public Season() { }
    }
}
