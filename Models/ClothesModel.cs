using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public class ClothesModel
    {
        public string Categorie { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
        public string Size { get; }
        public int Quantity { get; set; }
        public double Prize { get; set; }
        public string Comment { get; set; }
        public int OwnerId { get; }
        public string OwnerFirstname { get; }
        public string OwnerLastname { get; }

        public ClothesModel(string categorie, string name, string size, string season, int quantity, double prize)
        {
            Categorie = categorie;
            Name = name;
            Size = size;
            Season = season;
            Quantity = quantity;
            Prize = prize;
        }

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
