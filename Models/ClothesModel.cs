using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public enum Categorie { Hose, Pullover, Shirt, Jacke, Kopfbedeckung }
    public enum Season { Sommer, Winter, Saisonlos}

    public class ClothesModel(Categorie categorie, string name, string size, Season season, int quantity)
    {
        public Categorie Categorie { get; set; } = categorie;
        public string Name { get; set; } = name;
        public Season Season { get; set; } = season;
        public string Size { get; } = size;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; } = "";
    }
}
