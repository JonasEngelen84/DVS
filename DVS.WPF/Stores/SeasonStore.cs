using DVS.Domain.Models;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class SeasonStore
    {
        private readonly List<SeasonModel> _seasons;
        public IEnumerable<SeasonModel> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<SeasonModel, AddEditSeasonFormViewModel> SeasonAdded;
        public event Action<SeasonModel, AddEditSeasonFormViewModel> SeasonEdited;
        public event Action<SeasonModel, AddEditSeasonFormViewModel> SeasonDeleted;
        public event Action<AddEditSeasonFormViewModel> AllSeasonsDeleted;


        public SeasonStore()
        {
            _seasons = [new(Guid.NewGuid(), "Sommer"), new(Guid.NewGuid(),
                "Winter"), new(Guid.NewGuid(), "Saisonlos")];
        }


        public async Task Load()
        {
            _seasons.Clear();
            SeasonsLoaded?.Invoke();
        }

        public async Task Add(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            _seasons.Add(season);
            SeasonAdded.Invoke(season, addEditSeasonFormViewModel);
        }

        public async Task Edit(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            int index = _seasons.FindIndex(y => y.GuidID == season.GuidID);

            if (index > -1)
            {
                _seasons[index] = season;
                SeasonEdited.Invoke(season, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        public async Task Delete(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToDelete != null)
            {
                _seasons.Remove(season);
                SeasonDeleted.Invoke(season, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }

        public async Task ClearSeasons(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            if (_seasons != null)
            {
                _seasons.Clear();
                AllSeasonsDeleted.Invoke(addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Saisons nicht möglich.");
            }
        }
    }
}
