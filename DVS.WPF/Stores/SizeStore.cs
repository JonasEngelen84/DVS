using DVS.Domain.Commands.SizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class SizeStore(IGetAllSizesQuery getAllSizesQuery, IUpdateSizeCommand updateSizeCommand)
    {
        private readonly IGetAllSizesQuery _getAllSizesQuery = getAllSizesQuery;
        private readonly IUpdateSizeCommand _updateSizeCommand = updateSizeCommand;

        private readonly List<SizeModel> _sizes = [];
        public IEnumerable<SizeModel> Sizes => _sizes;

        public async Task Load()
        {
            IEnumerable<SizeModel> sizes = await _getAllSizesQuery.Execute();

            _sizes.Clear();

            if (sizes != null)
            {
                _sizes.AddRange(sizes);
            }
        }

        public async Task Update(SizeModel updatedSize)
        {
            await _updateSizeCommand.Execute(updatedSize);

            int index = _sizes.FindIndex(y => y.GuidId == updatedSize.GuidId);

            if (index != -1)
            {
                _sizes[index] = updatedSize;
            }
            else
            {
                _sizes.Add(updatedSize);
            }
        }
    }
}
