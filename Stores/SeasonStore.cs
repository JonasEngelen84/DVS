using DVS.Models;

namespace DVS.Stores
{
    public class SeasonStore
    {
        private readonly List<SeasonModel> _seasons;
        public IEnumerable<SeasonModel> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<SeasonModel> SeasonAdded;
        public event Action<SeasonModel, string> SeasonEdited;
        public event Action<SeasonModel> SeasonDeleted;
        public event Action AllSeasonsDeleted;


        public SeasonStore()
        {
            _seasons = [new("Sommer"), new("Winter"), new("Saisonlos")];
        }


        public async Task Load()
        {
            _seasons.Clear();
            SeasonsLoaded?.Invoke();
        }

        public async Task Add(SeasonModel season)
        {//TODO: Bedingung zum Adden hinzufügen
            SeasonAdded.Invoke(season);
            _seasons.Add(season);
        }

        public async Task Edit(SeasonModel oldSeason, string editedSeason)
        {
            var seasonToUpdate = _seasons.FirstOrDefault(y => y.Name == oldSeason.Name);

            if (seasonToUpdate != null)
            {
                SeasonEdited.Invoke(oldSeason, editedSeason);
                seasonToUpdate.Name = editedSeason;
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        public async Task Delete(SeasonModel season)
        {
            var seasonToDelete = _seasons.FirstOrDefault(y => y.Name == season.Name);

            if (seasonToDelete != null)
            {
                SeasonDeleted.Invoke(season);
                _seasons.Remove(seasonToDelete);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }
        public async Task ClearSeasons()
        {
            if (_seasons != null)
            {
                AllSeasonsDeleted.Invoke();
                _seasons.Clear();
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Saisons nicht möglich.");
            }
        }
    }
}
