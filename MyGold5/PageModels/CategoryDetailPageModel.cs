using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Core;
using MyGold5.Models;

namespace MyGold5.PageModels
{
    public partial class CategoryDetailPageModel(CategoryRepository repository) : ObservableObject, IQueryAttributable
    {
        private Category? _item;

        [ObservableProperty]
        string _name = string.Empty;

        [ObservableProperty]
        Category? _category;

        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        bool _canDelete;

        async Task LoadData(int id)
        {
            try
            {
                IsBusy = true;

                _item = await repository.GetAsync(id);
                Name = _item.Name;
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
            }
        }
    }
}
