using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Season
    {
        public Guid GuidId { get; }
        public string Name { get; }

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
