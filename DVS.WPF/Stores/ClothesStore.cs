using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class ClothesStore(ICreateClothesCommand createClothesCommand,
                              IUpdateClothesCommand updateClothesCommand,
                              IDeleteClothesCommand deleteClothesCommand)
    {
        private readonly List<Clothes> _clothes = [];
        public IEnumerable<Clothes> Clothes => _clothes;

        public event Action<Clothes> ClothesAdded;
        public event Action<Clothes> ClothesUpdated;
        public event Action<string> ClothesDeleted;

        public void Load(List<Clothes> clothes)
        {
            _clothes.Clear();

            if (clothes != null)
            {
                _clothes.AddRange(clothes);
            }
        }

        public async Task Add(Clothes clothes)
        {
            await createClothesCommand.Execute(clothes);

            _clothes.Add(clothes);

            ClothesAdded.Invoke(clothes);
        }

        public async Task Update(Clothes updatedClothes)
        {
            await updateClothesCommand.Execute(updatedClothes);

            int index = _clothes.FindIndex(c => c.Id == updatedClothes.Id);

            if (index != -1)
            {
                _clothes[index] = updatedClothes;
            }
            else
            {
                _clothes.Add(updatedClothes);
            }

            ClothesUpdated.Invoke(updatedClothes);
        }

        public async Task Delete(Clothes clothes)
        {
            await deleteClothesCommand.Execute(clothes);

            int index = _clothes.FindIndex(c => c.Id == clothes.Id);

            if (index != -1)
            {
                _clothes.RemoveAll(c => c.Id == clothes.Id);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Bekleidung nicht möglich.");
            }            

            ClothesDeleted?.Invoke(clothes.Id);
        }
    }
}
