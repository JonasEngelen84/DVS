namespace DVS.Models
{
    public class CategoryModel(string name) : ModelBase
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
