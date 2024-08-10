using DVS.Domain.Commands.Season;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.EntityFramework.Queries;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class SeasonStore(IGetAllSeasonsQuery getAllSeasonsQuery,
                             ICreateSeasonCommand CreateSeasonCommand,
                             IUpdateSeasonCommand UpdateSeasonCommand,
                             IDeleteSeasonCommand DeleteSeasonCommand)
    {
        private readonly IGetAllSeasonsQuery _getAllSeasonsQuery = getAllSeasonsQuery;
        private readonly ICreateSeasonCommand _createSeasonCommand = CreateSeasonCommand;
        private readonly IUpdateSeasonCommand _updateSeasonCommand = UpdateSeasonCommand;
        private readonly IDeleteSeasonCommand _deleteSeasonCommand = DeleteSeasonCommand;

        private readonly List<Season> _seasons = [];
        public IEnumerable<Season> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<Season, AddEditSeasonFormViewModel> SeasonAdded;
        public event Action<Season, AddEditSeasonFormViewModel> SeasonUpdated;
        public event Action<Guid, AddEditSeasonFormViewModel> SeasonDeleted;
        public event Action<AddEditSeasonFormViewModel> AllSeasonsDeleted;

        public async Task Load()
        {
            try
            {
                //IEnumerable<Season> season = await _getAllSeasonsQuery.Execute();

                _seasons.Clear();

                //if (season != null)
                //{
                //    _seasons.AddRange(season);
                //}

                SeasonsLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                //TODO: Fehlerbehandlung beim laden der aus DB
                Console.WriteLine($"Fehler beim Laden der Saisons: {ex.Message}");
            }
        }

        public async Task Add(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            //await _createSeasonCommand.Execute(season);
            _seasons.Add(season);
            SeasonAdded.Invoke(season, addEditSeasonFormViewModel);
        }

        public async Task Update(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            //await _updateSeasonCommand.Execute(season);

            int index = _seasons.FindIndex(y => y.GuidID == season.GuidID);

            if (index > -1)
            {
                _seasons[index] = season;
                SeasonUpdated.Invoke(season, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        public async Task Delete(Guid guidID, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            //await _deleteSeasonCommand.Execute(guidID);

            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == guidID);

            if (seasonToDelete != null)
            {
                _seasons.RemoveAll(y => y.GuidID == guidID);
                SeasonDeleted.Invoke(guidID, addEditSeasonFormViewModel);
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
