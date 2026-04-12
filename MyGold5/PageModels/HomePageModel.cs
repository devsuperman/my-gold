using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyGold5.Models;

namespace MyGold5.PageModels;
public partial class HomePageModel(SpentRepository repository) : ObservableObject
{

    [ObservableProperty]
    private List<Spent> _list = [];

    [RelayCommand]
    private async Task Appearing()
    {
        List = await repository.ListAsync(DateTime.Today, DateTime.Today, 0);
    }
}