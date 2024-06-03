using DVS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.ViewModels.ListViewItems
{
    public class CLothesListViewItemViewModel : ViewModelBase
    {
        public ClothesModel ClothesModel { get; private set; }

        public CLothesListViewItemViewModel(ClothesModel clothesModel)
        {
            ClothesModel = clothesModel;
        }

        public int Id => ClothesModel.Id;
        public string Name => ClothesModel.Name;
        public string Categorie => ClothesModel.Categorie;
        public string Season => ClothesModel.Season;
        public string Size => ClothesModel.Size;
        public int Quantity => ClothesModel.Quantity;
        public string Comment => ClothesModel.Comment;
    }
}
