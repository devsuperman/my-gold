namespace MyGold5.Models;
public class TotalByCategory
{
    public int CategoryId { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Total { get; set; }
}