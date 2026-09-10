using MyGold5.PageModels;

namespace MyGold5.Pages;
public partial class CategoryDetailPage : ContentPage
{
    public CategoryDetailPage(CategoryDetailPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}
