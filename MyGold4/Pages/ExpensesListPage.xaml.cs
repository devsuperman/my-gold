namespace MyGold4.Pages
{
    public partial class ExpensesListPage : ContentPage
    {
        public ExpensesListPage(ProjectListPageModel model)
        {
            BindingContext = model;
            InitializeComponent();
        }
    }
}