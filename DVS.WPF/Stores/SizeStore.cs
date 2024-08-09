using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class SizeStore/*(IGetAllSizesQuery getAllSizesQuery)*/
    {
        private readonly List<SizeModel> _sizes;
        public IEnumerable<SizeModel> Sizes => _sizes;

        //private readonly IGetAllSizesQuery _getAllSizesQuery;

        public SizeStore()
        {
            _sizes =
            [
                new SizeModel("44", true),
                new SizeModel("46", true),
                new SizeModel("48", true),
                new SizeModel("50", true),
                new SizeModel("52", true),
                new SizeModel("54", true),
                new SizeModel("56", true),
                new SizeModel("58", true),
                new SizeModel("60", true),
                new SizeModel("62", true),
                new SizeModel("XS", false),
                new SizeModel("S", false),
                new SizeModel("M", false),
                new SizeModel("L", false),
                new SizeModel("XL", false),
                new SizeModel("XLL", false),
                new SizeModel("3XL", false),
                new SizeModel("4XL", false),
                new SizeModel("5XL", false),
                new SizeModel("6XL", false)
            ];
        }

        public async Task Load()
        {
            //await _getAllSizesQuery.Execute();
            _sizes.Clear();
        }
    }
}
