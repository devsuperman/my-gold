using MyGold5.PageModels;

namespace MyGold5.Pages;
public partial class SpentDetailPage : ContentPage
{
    public SpentDetailPage(SpentDetailPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}
