namespace MyGold5.Pages;
public partial class HomePage : ContentPage
{
    public HomePage(HomePageModel model)
    {
        BindingContext = model;
        InitializeComponent();
    }
}