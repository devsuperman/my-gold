using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;
using MyGold5.Models;
using MyGold5.Data;

namespace MyGold5.PageModels
{
    public partial class SpentDetailPageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject
    {
        private Spent? _item;

        [ObservableProperty]
        string _name = string.Empty;

        [ObservableProperty]
        decimal _value;

        [ObservableProperty]
        int _categoryId;

        [ObservableProperty]
        DateTime _date;

        [ObservableProperty]
        List<Category> _categories = [];

        [ObservableProperty]
        Category? _category;

        [ObservableProperty]
        private int _categoryIndex = -1;

        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        bool _canDelete;

        async Task LoadCategories() => Categories = await categoryRepository.ListAsync();

        async Task LoadData(int id)
        {
            try
            {
                IsBusy = true;

                _item = await repository.GetAsync(id);

                Name = _item.Name;
                Value = _item.Value;
                Date = _item.Date;
                CategoryId = _item.CategoryId;

                Categories = await categoryRepository.ListAsync();

                Category = Categories?.FirstOrDefault(c => c.ID == _item.CategoryId);
                CategoryIndex = Categories?.FindIndex(c => c.ID == _item.CategoryId) ?? -1;

            }
            catch (Exception e)
            {
                await Toast.Make(e.Message, ToastDuration.Long).Show();
            }
            finally
            {
                CanDelete = _item.ID > 0;
                IsBusy = false;
            }
        }


        [RelayCommand]
        async Task Save()
        {
            _item.Name = Name;
            _item.Value = Value;
            _item.Date = Date;
            _item.CategoryId = Category?.ID ?? 0;

            await repository.SaveItemAsync(_item);
            await Shell.Current.GoToAsync("..");
            await AppShell.DisplayToastAsync("Saved");
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        async Task Delete()
        {
            await repository.DeleteItemAsync(_item);
            await Shell.Current.GoToAsync("..");
            await AppShell.DisplayToastAsync("Deleted");
        }
    }
}
