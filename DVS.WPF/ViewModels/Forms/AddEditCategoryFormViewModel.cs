﻿using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEditCategoryFormViewModel(
        ICommand addCategoryCommand,
        ICommand editCategoryCommand,
        ICommand deleteCategoryCommand,
        SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel)
        : ViewModelBase
    {
        public SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel { get; } = SizesCategoriesSeasonsListingViewModel;
        public ICommand AddCategory { get; } = addCategoryCommand;
        public ICommand EditCategory { get; } = editCategoryCommand;
        public ICommand DeleteCategory { get; } = deleteCategoryCommand;

        private string _newCategory;
        public string NewCategory
        {
            get => _newCategory;
            set
            {
                _newCategory = value;
                OnPropertyChanged(nameof(NewCategory));
                OnPropertyChanged(nameof(CanAdd));
            }
        }
        
        private string _editSelectedCategory;
        public string EditSelectedCategory
        {
            get => _editSelectedCategory;
            set
            {
                _editSelectedCategory = value;
                OnPropertyChanged(nameof(EditSelectedCategory));
                OnPropertyChanged(nameof(CanEdit));
            }
        }
        
        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (value != null)
                {
                    _selectedCategory = value;
                    EditSelectedCategory = new(value.Name);
                    OnPropertyChanged(nameof(SelectedCategory));
                    OnPropertyChanged(nameof(CanDelete));
                }
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
        
        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        public bool HasError;

        public bool CanAdd =>
            !string.IsNullOrEmpty(NewCategory) &&
            !NewCategory.Equals("Neue Kategorie");

        public bool CanEdit =>
            !string.IsNullOrEmpty(EditSelectedCategory) &&
            !SelectedCategory.Name.Equals("Kategorie wählen") &&
            !SelectedCategory.Name.Equals(EditSelectedCategory);

        public bool CanDelete => !SelectedCategory.Name.Equals("Kategorie wählen");
    }
}
