using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Category
    {
        public Guid GuidID { get; }
        public string Name { get; private set; }

        public ObservableCollection<Clothes> Clothes { get; set; }

        public Category(Guid guidID, string name)
        {
            GuidID = guidID;
            Name = name;

            Clothes = [];
        }

        public Category()
        {

        }
    }
}
