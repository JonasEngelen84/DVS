﻿using DVS.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DVS.WPF.Components
{
    public partial class AddEditEmployeeNewEmployeeClothesList : UserControl
    {
        public static readonly DependencyProperty IncomingClothesItemProperty =
            DependencyProperty.Register("IncomingClothesItem", typeof(object), typeof(AddEditEmployeeNewEmployeeClothesList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object IncomingClothesItem
        {
            get { return GetValue(IncomingClothesItemProperty); }
            set { SetValue(IncomingClothesItemProperty, value); }
        }

        public static readonly DependencyProperty ClothesItemDropCommandProperty =
            DependencyProperty.Register("ClothesItemDropCommand", typeof(ICommand), typeof(AddEditEmployeeNewEmployeeClothesList),
                new PropertyMetadata(null));

        public ICommand ClothesItemDropCommand
        {
            get { return (ICommand)GetValue(ClothesItemDropCommandProperty); }
            set { SetValue(ClothesItemDropCommandProperty, value); }
        }

        public static readonly DependencyProperty ClothesItemRemovedCommandProperty =
            DependencyProperty.Register("ClothesItemRemovedCommand", typeof(ICommand), typeof(AddEditEmployeeNewEmployeeClothesList),
                new PropertyMetadata(null));

        public ICommand ClothesItemRemovedCommand
        {
            get { return (ICommand)GetValue(ClothesItemRemovedCommandProperty); }
            set { SetValue(ClothesItemRemovedCommandProperty, value); }
        }


        public AddEditEmployeeNewEmployeeClothesList()
        {
            InitializeComponent();
        }


        //TODO: canMove DRINGEND ausbessern
        private bool canMove = true;
        private void ClothesItem_MouseMove(object sender, MouseEventArgs e)
        {
            canMove = true;

            if (e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                canMove = false;
                object ClothesItem = frameworkElement.DataContext;

                DragDropEffects dragDropResult = DragDrop.DoDragDrop(frameworkElement,
                    new DataObject(DataFormats.Serializable, ClothesItem),
                    DragDropEffects.Move);

                if (dragDropResult == DragDropEffects.None)
                {
                    AddClothesItem(ClothesItem);
                }
            }
        }

        private void ClothesItemList_Drop(object sender, DragEventArgs e)
        {
            if (canMove)
            {
                if (e.Data.GetData(DataFormats.Serializable) is DetailedClothesListingItemViewModel ClothesItem)
                {
                    if (ClothesItemRemovedCommand?.CanExecute(null) ?? false)
                    {
                        IncomingClothesItem = e.Data.GetData(DataFormats.Serializable);
                        AddClothesItem(ClothesItem);
                        ClothesItemRemovedCommand?.Execute("EmployeeClothesList");
                    }
                }
            }
            canMove = true;
        }

        private void AddClothesItem(object ClothesItem)
        {
            if (ClothesItemDropCommand?.CanExecute(null) ?? false)
            {
                IncomingClothesItem = ClothesItem;
                ClothesItemDropCommand?.Execute("EmployeeClothesList");
            }
        }
    }
}
