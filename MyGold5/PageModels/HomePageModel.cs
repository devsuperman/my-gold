using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class HomePageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject
{
    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    List<TotalByCategory> _totals = [];

    [RelayCommand]
    private async Task Appearing()
    {
        Categories = await categoryRepository.ListAsync();
        var expenses = await repository.ListAsync(DateTime.Today, DateTime.Today, 0);

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