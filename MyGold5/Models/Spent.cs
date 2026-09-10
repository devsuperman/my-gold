using SQLite;

namespace MyGold5.Models;

public class Spent
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }


    [Indexed]
    public DateTime Date { get; set; }


    [Indexed]
    public int CategoryId { get; set; }

    public string Description
    {
        get
        {
            return $"{Date:dd/MM} - {Name}";
        }
    }
}
