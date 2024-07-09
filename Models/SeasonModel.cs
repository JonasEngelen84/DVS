namespace DVS.Models
{
    public class SeasonModel(string name) : ModelBase
    {
        private string _name = name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
