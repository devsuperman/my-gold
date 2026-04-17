using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class SpentListPageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject
{
    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    Category _selectedCategory;

    [ObservableProperty]
    List<SpentGroup> _listGroup = [];

    [ObservableProperty]
    Spent? selectedItem;

    [ObservableProperty]
    DateTime _month = DateTime.Today;

    [ObservableProperty]
    string _title;

    [RelayCommand]
    async Task Appearing()
    {
        var categories = await categoryRepository.ListAsync();
        Categories = [.. categories, new() { ID = 0, Name = "😎 All" }];

        await LoadExpenses();
    }

    [RelayCommand]
    async Task NextMonth()
    {
        Month = Month.AddMonths(1);
        await LoadExpenses();
    }

    [RelayCommand]
    async Task PreviousMonth()
    {
        Month = Month.AddMonths(-1);
        await LoadExpenses();
    }

    [RelayCommand]
    async Task LoadExpenses()
    {
        Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Month.ToString("MMMM"));

        if (DateTime.Today.Year != Month.Year)
            Title = $"{Title} de {Month.Year}";

        var expenses = await repository.ListAsync(Month, SelectedCategory?.ID ?? 0);

        foreach (var item in expenses)
        {
            var c = Categories.FirstOrDefault(f => f.ID == item.CategoryId);
            item.Name = $"{c?.Name} {item.Name}";
        }

        ListGroup = expenses
            .GroupBy(g => g.Date)
            .Select(s => new SpentGroup(s.Key.ToString("dd - dddd"), s.ToList()))
            .ToList();
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