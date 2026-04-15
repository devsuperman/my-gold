using MyGold5.Models;
using SQLite;

namespace MyGold5.Data;

public class SpentRepository
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database is not null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await database.CreateTableAsync<Spent>();        
    }

    public async Task<List<Spent>> ListAsync(DateTime monthYear, int categoryId)
    {
        await Init();

        var start = new DateTime(monthYear.Year, monthYear.Month, 1);
        var end = start.AddMonths(1).AddTicks(-1);

        var list = await database.Table<Spent>()
            .Where(w=> 
                (categoryId == 0 || categoryId == w.CategoryId) &&
                start <= w.Date && w.Date <= end)
            .ToListAsync();

        return list;
    }

    public async Task<Spent?> GetAsync(int id)
    {
        await Init();
        return await database.Table<Spent>().FirstOrDefaultAsync(f => f.ID == id);
    }

    public async Task<int> SaveItemAsync(Spent item)
    {
        await Init();

        if (item.ID == 0)
            await database.InsertAsync(item);
        else
            await database.UpdateAsync(item);

        return item.ID;
    }

    public async Task DeleteItemAsync(Spent item)
    {
        await Init();
        await database.DeleteAsync(item);
    }
}