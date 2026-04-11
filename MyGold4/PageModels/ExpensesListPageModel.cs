using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold4.Models;

namespace MyGold4.PageModels
{
    public partial class ExpensesListPageModel(ExpensesRepository repository) : ObservableObject
    {
        [ObservableProperty]
        private List<Spent> _list = [];

        [ObservableProperty]
        private Spent? selectedItem;

        [RelayCommand]
        private async Task AppearingAsync()
        {
            var start = DateOnly.FromDateTime(DateTime.Today);
            var end = DateOnly.FromDateTime(DateTime.Today);
            var categoryId = 0;
            List = await repository.ListAsync(start, end, categoryId);
        }

        [RelayCommand]
        Task? OpenItemAsync(Spent item) => item is null ? Task.CompletedTask : Shell.Current.GoToAsync($"spent?id={item.ID}");

        [RelayCommand]
        async Task AddAsync() => await Shell.Current.GoToAsync($"spent");
    }
}