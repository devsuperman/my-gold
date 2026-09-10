using MyGold5.Models;
using SQLite;

namespace MyGold5.Data;

public class CategoryRepository()
{
    SQLiteAsyncConnection db;

    private async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await db.CreateTableAsync<Category>();        
    }

    public async Task<List<Category>> ListAsync()
    {
        await Init();
        return await db.Table<Category>().ToListAsync();
    }

    public async Task<Category?> GetAsync(int id)
    {
        await Init();
        return await db.Table<Category>().FirstOrDefaultAsync(f => f.ID == id);
    }

    public async Task<int> SaveItemAsync(Category item)
    {
        await Init();

        if (item.ID == 0)
            await db.InsertAsync(item);
        else
            await db.UpdateAsync(item);

        return item.ID;
    }

    public async Task DeleteItemAsync(Category item)
    {
        await Init();
        await db.DeleteAsync(item);
    }
}