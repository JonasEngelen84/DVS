﻿using System.Windows;
using System.Windows.Controls;

namespace DVS.WPF.Views.Components.ListingItems
{
    public partial class ClothesListingItem : UserControl
    {
        public ClothesListingItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dropdown.IsOpen = false;
        } 
    }
}
