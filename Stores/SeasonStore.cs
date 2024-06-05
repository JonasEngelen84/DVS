using System.Collections.ObjectModel;

namespace DVS.Stores
{
    public class SeasonStore
    {
        private readonly ObservableCollection<String> _seasons;
        public IEnumerable<string> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<string> SeasonsAdded;

        public SeasonStore()
        {
            _seasons = ["Saisonlos", "Sommer", "Winter"];
        }

        public async Task Load()
        {
            SeasonsLoaded?.Invoke();
        }

        public async Task Add(string season)
        {
            _seasons.Add(season);
            SeasonsAdded?.Invoke(season);
        }
    }
}
