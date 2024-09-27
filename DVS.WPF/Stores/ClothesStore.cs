using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using System.Windows;

namespace DVS.WPF.Stores
{
    public class ClothesStore(IGetAllClothesQuery getAllClothesQuery,
                              ICreateClothesCommand createClothesCommand,
                              IUpdateClothesCommand updateClothesCommand,
                              IDeleteClothesCommand deleteClothesCommand)
    {
        private readonly IGetAllClothesQuery _getAllClothesQuery = getAllClothesQuery;
        private readonly ICreateClothesCommand _createClothesCommand = createClothesCommand;
        private readonly IUpdateClothesCommand _updateClothesCommand = updateClothesCommand;
        private readonly IDeleteClothesCommand _deleteClothesCommand = deleteClothesCommand;

        private readonly List<Clothes> _clothes = [];
        public IEnumerable<Clothes> Clothes => _clothes;

        public event Action ClothesLoaded;
        public event Action<Clothes> ClothesAdded;
        public event Action<Clothes> ClothesUpdated;
        public event Action<Guid> ClothesDeleted;

        public async Task Load()
        {
            IEnumerable<Clothes> clothes = [];

            try
            {
                clothes = await _getAllClothesQuery.Execute();
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der Clothes von Datenbank ist fehlgeschlagen!", "ClothesStore, Load", button, icon);
            }

            _clothes.Clear();

            if (clothes != null)
            {
                _clothes.AddRange(clothes);
            }

            ClothesLoaded?.Invoke();
        }

        public async Task Add(Clothes clothes)
        {
            try
            {
                await _createClothesCommand.Execute(clothes);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Hinzufügen der Clothes in Datenbank ist fehlgeschlagen!", "ClothesStore, Add", button, icon);
            }

            _clothes.Add(clothes);

            ClothesAdded.Invoke(clothes);
        }

        public async Task Update(Clothes clothes)
        {
            try
            {
                //await _updateClothesCommand.Execute(clothes);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten des Clothes in Datenbank ist fehlgeschlagen!", "ClothesStore, Update", button, icon);
            }

            int index = _clothes.FindIndex(y => y.GuidID == clothes.GuidID);

            if (index != -1)
            {
                _clothes[index] = clothes;
            }
            else
            {
                _clothes.Add(clothes);
            }

            ClothesUpdated.Invoke(clothes);
        }

        public async Task Delete(Clothes clothes)
        {
            try
            {
                await _deleteClothesCommand.Execute(clothes);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Löschen der Clothes aus Datenbank ist fehlgeschlagen!", "ClothesStore, Delete", button, icon);
            }

            _clothes.RemoveAll(y => y.GuidID == clothes.GuidID);

            ClothesDeleted?.Invoke(clothes.GuidID);
        }
    }
}
