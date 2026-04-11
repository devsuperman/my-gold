namespace MyGold4.Pages;
public partial class SpentDetailPage : ContentPage
{
    public SpentDetailPage(SpentDetailPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}
