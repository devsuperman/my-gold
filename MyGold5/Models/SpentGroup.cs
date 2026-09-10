namespace MyGold5.Models;
public class SpentGroup : List<Spent>
{
    public string Name { get; private set; }
    public SpentGroup(string name, List<Spent> expenses) : base(expenses)
    {
        Name = name;
    }
}
