using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold5.Models;
using MyGold5.Data;

namespace MyGold5.PageModels
{
    public partial class SpentListPageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject
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
            
            var categories = await categoryRepository.ListAsync();
            var expenses = await repository.ListAsync(start, end, categoryId);

            foreach (var item in expenses)
            {
                var c = categories.FirstOrDefault(f => f.ID == item.CategoryId);
                item.Name = $"{c.Name} {item.Name}";
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
}