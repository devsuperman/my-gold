using CommunityToolkit.Mvvm.Input;
using MyGold4.Models;

namespace MyGold4.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}