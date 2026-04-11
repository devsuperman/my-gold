using MyGold4.Models;
using MyGold4.PageModels;

namespace MyGold4.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}