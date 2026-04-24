using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using MyGold5.Models;

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

    [ObservableProperty]
    decimal _total;

    [RelayCommand]
    async Task Appearing()
    {
        Categories = await categoryRepository.ListAsync();
        await LoadExpenses();
    }

    [RelayCommand]
    async Task LoadExpenses()
    {
        Title = "Resumo de " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Month.ToString("MMMM"));

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

        Totals = [.. listTotals.OrderByDescending(o => o.Total).ToList()];
        Total = expenses.Sum(s => s.Value);
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
}