using MyGold5.PageModels;

namespace MyGold5.Pages;
public partial class CategoryListPage : ContentPage
{
    public CategoryListPage(CategoryListPageModel model)
    {
        BindingContext = model;
        InitializeComponent();
    }
}