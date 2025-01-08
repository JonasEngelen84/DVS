﻿using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize
    {
        public Guid GuidId { get; }
        public Guid ClothesGuidId { get; private set; }
        public Guid SizeGuidId { get; private set; }
        public int Quantity { get; private set; }
        public string Comment { get; private set; }
        public Clothes Clothes { get; private set; }
        public SizeModel Size { get; private set; }

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        public ClothesSize(Guid guidId, Clothes clothes, SizeModel size, int quantity, string comment)
        {
            GuidId = guidId;
            Clothes = clothes;
            Size = size;
            ClothesGuidId = clothes.GuidId;
            SizeGuidId = size.GuidId;
            Quantity = quantity;
            Comment = comment;

            EmployeeClothesSizes = [];
        }

        public ClothesSize()
        {

        }
    }
}
