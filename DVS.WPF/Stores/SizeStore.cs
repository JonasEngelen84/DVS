using DVS.Domain.Commands.SizeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class SizeStore(IUpdateSizeCommand updateSizeCommand)
    {
        private readonly IUpdateSizeCommand _updateSizeCommand = updateSizeCommand;

        private readonly List<SizeModel> _sizes = [];
        public IEnumerable<SizeModel> Sizes => _sizes;

        public void Load(List<SizeModel> sizes)
        {
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
