﻿using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Season(Guid? guidID, string name)
    {
        public Guid? GuidID { get; } = guidID;
        public string Name { get; } = name;

        public ObservableCollection<Clothes> Clothes { get; set; } = [];
    }
}
