using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class SpentListPageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject
{
    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    Category _selectedCategory;

    [ObservableProperty]
    List<Spent> _list = [];

    [ObservableProperty]
    Spent? selectedItem;

    [ObservableProperty]
    DateTime _month = DateTime.Today;

    [RelayCommand]
    async Task Appearing()
    {
        var categories = await categoryRepository.ListAsync();
        Categories = [.. categories, new() { ID = 0, Name = "😎 All" }];

        await LoadExpenses();
    }

    [RelayCommand]
    async Task LoadExpenses()
    {
        var expenses = await repository.ListAsync(Month, SelectedCategory?.ID ?? 0);

        foreach (var item in expenses)
        {
            var c = Categories.FirstOrDefault(f => f.ID == item.CategoryId);
            item.Name = $"{c?.Name} {item.Name}";
        }

        List = expenses;
    }

    [RelayCommand]
    async Task OpenItem(Spent item)
    {
        await Shell.Current.GoToAsync($"spent?id={item.ID}");
    }

    [RelayCommand]
    async Task AddItemAsync()
    {
        await Shell.Current.GoToAsync($"spent");
    }
}