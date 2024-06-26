using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DVS.Models;

namespace DVS.Components
{
    public partial class AddEditEmployeClothesList : UserControl
    {
        public static readonly DependencyProperty IncomingClothesItemProperty =
            DependencyProperty.Register("IncomingClothesItem", typeof(object), typeof(AddEditEmployeClothesList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object IncomingClothesItem
        {
            get { return GetValue(IncomingClothesItemProperty); }
            set { SetValue(IncomingClothesItemProperty, value); }
        }

        public static readonly DependencyProperty RemovedClothesItemProperty =
            DependencyProperty.Register("RemovedClothesItem", typeof(object), typeof(AddEditEmployeClothesList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object RemovedClothesItem
        {
            get { return GetValue(RemovedClothesItemProperty); }
            set { SetValue(RemovedClothesItemProperty, value); }
        }

        public static readonly DependencyProperty ClothesItemDropCommandProperty =
            DependencyProperty.Register("ClothesItemDropCommand", typeof(ICommand), typeof(AddEditEmployeClothesList),
                new PropertyMetadata(null));

        public ICommand ClothesItemDropCommand
        {
            get { return (ICommand)GetValue(ClothesItemDropCommandProperty); }
            set { SetValue(ClothesItemDropCommandProperty, value); }
        }

        public static readonly DependencyProperty ClothesItemRemovedCommandProperty =
            DependencyProperty.Register("ClothesItemRemovedCommand", typeof(ICommand), typeof(AddEditEmployeClothesList),
                new PropertyMetadata(null));

        public ICommand ClothesItemRemovedCommand
        {
            get { return (ICommand)GetValue(ClothesItemRemovedCommandProperty); }
            set { SetValue(ClothesItemRemovedCommandProperty, value); }
        }

        public static readonly DependencyProperty TargetClothesItemProperty =
            DependencyProperty.Register("TargetClothesItem", typeof(object), typeof(AddEditEmployeClothesList),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public object TargetClothesItem
        {
            get { return GetValue(TargetClothesItemProperty); }
            set { SetValue(TargetClothesItemProperty, value); }
        }

        //public static readonly DependencyProperty ClothesItemInsertedCommandProperty =
        //    DependencyProperty.Register("ClothesItemInsertedCommand", typeof(ICommand), typeof(AddEditEmployeClothesList),
        //        new PropertyMetadata(null));

        //public ICommand ClothesItemInsertedCommand
        //{
        //    get { return (ICommand)GetValue(ClothesItemInsertedCommandProperty); }
        //    set { SetValue(ClothesItemInsertedCommandProperty, value); }
        //}

        //public static readonly DependencyProperty InsertedClothesItemProperty =
        //    DependencyProperty.Register("InsertedClothesItem", typeof(object), typeof(AddEditEmployeClothesList),
        //        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //public object InsertedClothesItem
        //{
        //    get { return GetValue(InsertedClothesItemProperty); }
        //    set { SetValue(InsertedClothesItemProperty, value); }
        //}


        public AddEditEmployeClothesList()
        {
            InitializeComponent();
        }


        private void ClothesItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
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

        //private void ClothesItemList_DragLeave(object sender, DragEventArgs e)
        //{
        //    HitTestResult result = VisualTreeHelper.HitTest(lvItems, e.GetPosition(lvItems));

        //    if (result == null)
        //    {
        //        if (ClothesItemRemovedCommand?.CanExecute(null) ?? false)
        //        {
        //            RemovedClothesItem = e.Data.GetData(DataFormats.Serializable);
        //            ClothesItemRemovedCommand?.Execute(null);
        //        }
        //    }
        //}

        private void ClothesItemList_Drop(object sender, DragEventArgs e)
        {
            DetailedClothesListingItemModel? ClothesItem = e.Data.GetData(DataFormats.Serializable)
                                                           as DetailedClothesListingItemModel;
            
            if (ClothesItem != null)
            {
                if (ClothesItemRemovedCommand?.CanExecute(null) ?? false)
                {
                    RemovedClothesItem = e.Data.GetData(DataFormats.Serializable);
                    ClothesItemRemovedCommand?.Execute(null);
                    AddClothesItem(ClothesItem);
                }
            }
        }

        private void AddClothesItem(object ClothesItem)
        {
            if (ClothesItemDropCommand?.CanExecute(null) ?? false)
            {
                IncomingClothesItem = ClothesItem;
                ClothesItemDropCommand?.Execute(null);
            }
        }

        //private void ClothesItem_DragOver(object sender, DragEventArgs e)
        //{
        //    if (ClothesItemInsertedCommand?.CanExecute(null) ?? false)
        //    {
        //        if (sender is FrameworkElement element)
        //        {
        //            TargetClothesItem = element.DataContext;
        //            InsertedClothesItem = e.Data.GetData(DataFormats.Serializable);

        //            ClothesItemInsertedCommand?.Execute(null);
        //        }
        //    }
        //}
    }
}
