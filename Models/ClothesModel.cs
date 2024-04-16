using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public class ClothesModel(string categorie, string name, string size, string season, int quantity, double prize)
    {
        public string Categorie { get; set; } = categorie;
        public string Name { get; set; } = name;
        public string Season { get; set; } = season;
        public string Size { get; } = size;
        public int Quantity { get; set; } = quantity;
        public double Prize { get; set; } = prize;
        public string Comment { get; set; } = "";

        //public ClothesModel(string categorie, string name, string size, string season, int quantity, int ownerId, string ownerFirstname, string ownerLastname)
        //{
        //    Categorie = categorie;
        //    Name = name;
        //    Size = size;
        //    Season = season;
        //    Quantity = quantity;
        //    OwnerId = ownerId;
        //    OwnerFirstname = ownerFirstname;
        //    OwnerLastname = ownerLastname;
        //}
    }
}
