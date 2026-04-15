using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MyGold5.Models;
using System.Diagnostics;

namespace MyGold5.PageModels;

public partial class HomePageModel(SpentRepository repository, CategoryRepository categoryRepository, ILogger<HomePageModel> logger) : ObservableObject
{
    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    List<TotalByCategory> _totals = [];

    [ObservableProperty]
    DateTime _month = DateTime.Today;


    [RelayCommand]
    private async Task LoadData()
    {
        logger.LogInformation($"Selected Month: {Month:MMMM}");

        Categories = await categoryRepository.ListAsync();
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
    public async Task AddAsync()
    {
        await Shell.Current.GoToAsync("spent");
    }
}