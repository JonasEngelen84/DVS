using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public enum Categorie { Hose, Pullover, Shirt, Jacke, Kopfbedeckung }
    public enum Season { Sommer, Winter, Saisonlos }

    public class ClothesModel(int id, string name, Categorie categorie, string size, Season season, int quantity)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public Categorie Categorie { get; set; } = categorie;
        public Season Season { get; set; } = season;
        public string Size { get; } = size;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; } = "";
    }
}
