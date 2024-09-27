using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Season
    {
        public Guid GuidID { get; }
        public string Name { get; private set; }

        public ObservableCollection<Clothes> Clothes { get; set; }
            
        public Season(Guid guidID, string name)
        {
            GuidID = guidID;
            Name = name;

            Clothes = [];
        }

        public Season()
        {

        }
    }
}
