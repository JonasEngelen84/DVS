using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DVS.Stores
{
    public class SeasonStore
    {
        private readonly ObservableCollection<String> _seasons;
        private readonly CollectionViewSource _seasonCollectionViewSource;
        public IEnumerable<string> Seasons => _seasonCollectionViewSource.View.Cast<string>();

        public event Action SeasonsLoaded;
        public event Action<string> SeasonsAdded;


        public SeasonStore()
        {
            _seasons = ["Saisonlos", "Sommer", "Winter"];
            _seasonCollectionViewSource = new CollectionViewSource { Source = _seasons };
            _seasonCollectionViewSource.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
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
