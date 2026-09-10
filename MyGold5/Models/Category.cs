using SQLite;

namespace MyGold5.Models;
public class Category
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
}