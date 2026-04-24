using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui;
using System.Globalization;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class SpentListPageModel(SpentRepository repository, CategoryRepository categoryRepository, IPopupService popupService) : ObservableObject
{
    [ObservableProperty]
    List<SpentGroup> _listGroup = [];

    [ObservableProperty]
    Spent? selectedItem;

    [ObservableProperty]
    DateTime _month = DateTime.Today;

    [ObservableProperty]
    string _title;

    [ObservableProperty]
    decimal _total;

    int _selectedCategoryId;

    [RelayCommand]
    async Task Appearing()
    {
        await LoadExpenses();
    }

    [RelayCommand]
    async Task ThisMonth()
    {
        Month = DateTime.Today;
        await LoadExpenses();
    }

    [RelayCommand]
    async Task PreviousMonth()
    {
        Month = Month.AddMonths(-1);
        await LoadExpenses();
    }

    async Task LoadExpenses()
    {
        Title = "Gastos de " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Month.ToString("MMMM"));

        if (DateTime.Today.Year != Month.Year)
            Title = $"{Title} de {Month.Year}";

        var expenses = await repository.ListAsync(Month, _selectedCategoryId);
        var categories = await categoryRepository.ListAsync();

        foreach (var item in expenses)
        {
            var c = categories.FirstOrDefault(f => f.ID == item.CategoryId);
            item.Name = $"{c?.Name} {item.Name}";
        }

        ListGroup = expenses
            .GroupBy(g => g.Date)
            .Select(s => new SpentGroup(s.Key.ToString("dd - dddd"), s.ToList()))
            .ToList();

        Total = expenses.Sum(s => s.Value);
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

    [RelayCommand]
    async Task FilterByCategory()
    {
        var queryAttributes = new Dictionary<string, object>
        {
            ["categoryId"] = _selectedCategoryId
        };

        var popupResult = await popupService.ShowPopupAsync<FilterByCategoryPopupPageModel, int>(
            Shell.Current,
            options: PopupOptions.Empty,
            shellParameters: queryAttributes);

        _selectedCategoryId = popupResult.Result;

        await LoadExpenses();
    }
}