using MyGold5.Models;
using SQLite;

namespace MyGold5.Data;

public class SpentRepository()
{
    SQLiteAsyncConnection db;

    private async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await db.CreateTableAsync<Spent>();        
    }

    public async Task<List<Spent>> ListAsync(DateTime start, DateTime end, int categoryId)
    {
        await Init();

        var list = await db.Table<Spent>()
            .Where(w=> 
                (categoryId == 0 || categoryId == w.CategoryId) &&
                start <= w.Date && w.Date <= end)
            .ToListAsync();

        return list;
    }

    public async Task<Spent?> GetAsync(int id)
    {
        await Init();
        return await db.Table<Spent>().FirstOrDefaultAsync(f => f.ID == id);
    }

    public async Task<int> SaveItemAsync(Spent item)
    {
        await Init();

        if (item.ID == 0)
            await db.InsertAsync(item);
        else
            await db.UpdateAsync(item);

        return item.ID;
    }

    public async Task DeleteItemAsync(Spent item)
    {
        await Init();
        await db.DeleteAsync(item);
    }
}