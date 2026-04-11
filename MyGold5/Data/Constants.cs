namespace MyGold5.Data;
public static class Constants
{
    public const string DatabaseFilename = "AppSQLite2.db3";
    public static string DatabasePath => $"Data Source={Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename)}";

    public const SQLite.SQLiteOpenFlags Flags =
           SQLite.SQLiteOpenFlags.ReadWrite |
           SQLite.SQLiteOpenFlags.Create |
           SQLite.SQLiteOpenFlags.SharedCache;
}