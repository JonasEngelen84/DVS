﻿using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Models;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class SeasonStore(ICreateSeasonCommand createSeasonCommand,
                             IDeleteSeasonCommand deleteSeasonCommand)
    {
        private readonly List<Season> _seasons = [];
        public IEnumerable<Season> Seasons => _seasons;

        public event Action<Season> SeasonAdded;
        public event Action<Season> SeasonUpdated;
        public event Action<AddEditSeasonFormViewModel> SeasonDeleted;

        public void Load(List<Season> seasons)
        {
            _seasons.Clear();

            if (seasons != null)
            {
                _seasons.AddRange(seasons);
            }
        }

        public async Task Add(Season season)
        {
            await createSeasonCommand.Execute(season);

            _seasons.Add(season);

            SeasonAdded.Invoke(season);
        }

        public void Update(Season editedSeason)
        {
            int index = _seasons.FindIndex(y => y.Id == editedSeason.Id);

            if (index > -1)
            {
                _seasons[index] = editedSeason;
                SeasonUpdated.Invoke(editedSeason);
            }

            editedSeason.IsDirty = true;
        }

        public async Task Delete(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            await deleteSeasonCommand.Execute(addEditSeasonFormViewModel.SelectedSeason);

            int index = _seasons.FindIndex(y => y.Id == addEditSeasonFormViewModel.SelectedSeason.Id);

            if (index > -1)
            {
                _seasons.RemoveAll(y => y.Id == addEditSeasonFormViewModel.SelectedSeason.Id);
                SeasonDeleted.Invoke(addEditSeasonFormViewModel);
            }           
        }
    }
}
