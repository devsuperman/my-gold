namespace MyGold4.Pages;
public partial class SpentListPage : ContentPage
{
    public SpentListPage(SpentListPageModel model)
    {
        BindingContext = model;
        InitializeComponent();
    }
}