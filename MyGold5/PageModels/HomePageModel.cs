using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MyGold5.Models;
using System.Globalization;

namespace MyGold5.PageModels;

public partial class HomePageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject
{
    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    List<TotalByCategory> _totals = [];

    [ObservableProperty]
    DateTime _month = DateTime.Today;

    [ObservableProperty]
    string _title;

    [RelayCommand]
    async Task Appearing()
    {
        Categories = await categoryRepository.ListAsync();
        await LoadExpenses();
    }

    [RelayCommand]
    async Task LoadExpenses()
    {
        Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Month.ToString("MMMM"));

        if (DateTime.Today.Year != Month.Year)
            Title = $"{Title} de {Month.Year}";

        var expenses = await repository.ListAsync(Month, 0);

        var listTotals = expenses
            .GroupBy(g => g.CategoryId)
            .Select(g => new TotalByCategory
            {
                CategoryId = g.Key,
                Total = g.Sum(s => s.Value)
            })
            .ToList();

        foreach (var total in listTotals)
        {
            var category = Categories.FirstOrDefault(f => f.ID == total.CategoryId);
            total.Category = category?.Name;
        }

        Totals = new(listTotals);
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
    public async Task AddAsync()
    {
        await Shell.Current.GoToAsync("spent");
    }
}