using Syncfusion.Maui.Toolkit.Hosting;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MyGold5.Pages.Popups;

namespace MyGold5;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldEnableSnackbarOnWindows(true);
            })
            .ConfigureSyncfusionToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
            });

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddLogging(configure => configure.AddDebug());
#endif

        builder.Services.AddTransientWithShellRoute<HomePage, HomePageModel>("home");
        
        builder.Services.AddTransientWithShellRoute<CategoryListPage, CategoryListPageModel>("categories");
        builder.Services.AddTransientWithShellRoute<CategoryDetailPage, CategoryDetailPageModel>("category");

        builder.Services.AddTransientWithShellRoute<SpentListPage, SpentListPageModel>("expenses");
        builder.Services.AddTransientWithShellRoute<SpentDetailPage, SpentDetailPageModel>("spent");
        builder.Services.AddTransientPopup<FilterByCategoryPopup, FilterByCategoryPopupPageModel>();

        builder.Services.AddSingleton<SpentRepository>();
        builder.Services.AddSingleton<CategoryRepository>();

        return builder.Build();
    }
}
