using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class FilterByCategoryPopupPageModel(CategoryRepository categoryRepository, IPopupService popupService) : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public partial int CategoryId { get; set; }

    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    Category _selectedCategory;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CategoryId = (int)query["categoryId"];
        LoadCategories();
    }

    async Task LoadCategories()
    {
        Category categoryAll = new() { ID = 0, Name = "😎 All" };
        var categories = new List<Category> { categoryAll };
        categories.AddRange(await categoryRepository.ListAsync());

        Categories = [.. categories];
        SelectedCategory = categories.FirstOrDefault(f => f.ID == CategoryId);
    }

    [RelayCommand]
    async Task NewSelectedCategory(Category category)
    {
        await popupService.ClosePopupAsync(Shell.Current, category.ID);
    }
}