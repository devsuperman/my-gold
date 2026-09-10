using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class CategoryListPageModel(CategoryRepository repository) : ObservableObject
{
    [ObservableProperty]
    private List<Category> _list = [];

    [ObservableProperty]
    private Category? selectedItem;

    [RelayCommand]
    private async Task Appearing()
    {
        List = await repository.ListAsync();
    }

    [RelayCommand]
    async Task OpenItem(Category item)
    {
        await Shell.Current.GoToAsync($"category?id={item.ID}");
    }

    [RelayCommand]
    async Task AddItemAsync()
    {
        await Shell.Current.GoToAsync($"category");
    }
}