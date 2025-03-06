using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Season : ObservableEntity
    {
        public Guid GuidId { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Clothes> Clothes { get; set; }

        public Season(Guid guidId, string name)
        {
            GuidId = guidId;
            Name = name;

            Clothes = [];
        }

        public Season() {}
    }
}
