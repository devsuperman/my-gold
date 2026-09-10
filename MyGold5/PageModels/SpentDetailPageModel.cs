using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;
using MyGold5.Models;

namespace MyGold5.PageModels;

public partial class SpentDetailPageModel(SpentRepository repository, CategoryRepository categoryRepository) : ObservableObject, IQueryAttributable
{
    Spent _item = new();

    [ObservableProperty]
    string _name = string.Empty;

    [ObservableProperty]
    decimal? _value;

    [ObservableProperty]
    int _categoryId;

    [ObservableProperty]
    DateTime _date = DateTime.Today;

    [ObservableProperty]
    List<Category> _categories = [];

    [ObservableProperty]
    Category? _category;

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

            await LoadCategories();
            _item = await repository.GetAsync(id);

            Name = _item.Name;
            Value = _item.Value;
            Date = _item.Date;
            CategoryId = _item.CategoryId;
            Category = Categories.FirstOrDefault(c => c.ID == _item.CategoryId);
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
        _item.Name= Name;
        _item.Value = Value ?? 0;
        _item.Date = Date;
        _item.CategoryId = Category?.ID ?? 0;

        await repository.SaveItemAsync(_item);
        await Shell.Current.GoToAsync("..");
        await Toast.Make("Saved", ToastDuration.Long).Show();
    }

    [RelayCommand(CanExecute = nameof(CanDelete))]
    async Task Delete()
    {
        await repository.DeleteItemAsync(_item);
        await Shell.Current.GoToAsync("..");
        await Toast.Make("Deleted", ToastDuration.Long).Show();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("id"))
        {
            int id = Convert.ToInt32(query["id"]);
            LoadData(id);
        }
        else
        {
            _item = new();
            LoadCategories();
        }

    }
}
