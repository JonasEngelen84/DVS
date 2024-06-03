namespace DVS.Stores
{
    public class SeasonStore
    {
        private readonly List<String> _seasons;
        public IEnumerable<string> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<string> SeasonsAdded;

        public SeasonStore()
        {
            _seasons = [];
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
