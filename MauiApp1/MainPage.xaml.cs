namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        TodoItemDatabase database;

        public MainPage(TodoItemDatabase todoItemDatabase)
        {
            InitializeComponent();
            database = todoItemDatabase;
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            var ee = await database.GetItemsAsync();

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
