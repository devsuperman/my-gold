using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold5.Models;
using MyGold5.Data;

namespace MyGold5.PageModels
{
    public partial class SpentListPageModel(SpentRepository repository) : ObservableObject
    {
        [ObservableProperty]
        private List<Spent> _list = [];

        [ObservableProperty]
        private Spent? selectedItem;

        [RelayCommand]
        private async Task Appearing()
        {
            var start = DateTime.Today;
            var end = DateTime.Today;
            var categoryId = 0;
            List = await repository.ListAsync(start, end, categoryId);
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
}