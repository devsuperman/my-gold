using SQLite;

namespace MyGold4.Models;

public class Spent
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }


    [Indexed]
    public DateOnly Date { get; set; }


    [Indexed]
    public int CategoryId { get; set; }
}
