using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class CommentClothesSizeFormViewModel : ViewModelBase
    {
        private readonly SelectedClothesSizeStore _selectedClothesSizeStore;

        private ClothesSize SelectedClothesSize => _selectedClothesSizeStore.SelectedClothesSize;

        public bool HasSelectedClothesSize => SelectedClothesSize != null;
        public Clothes Clothes => SelectedClothesSize.Clothes;
        public string ID => Clothes.Id;
        public string Name => Clothes.Name;
        public string Category => Clothes.Category.Name;
        public string Season => Clothes.Season.Name;
        public string Size => SelectedClothesSize.Size.Size;
        public int Quantity => (int)SelectedClothesSize.Quantity;

        private string? _comment;
        public string? Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        public bool HasError;

        public ICommand SubmitComment { get; }

        public CommentClothesSizeFormViewModel(ICommand submitComment, SelectedClothesSizeStore selectedClothesSizeStore)
        {
            SubmitComment = submitComment;
            _selectedClothesSizeStore = selectedClothesSizeStore;

            _comment = SelectedClothesSize.Comment;
        }
    }
}
