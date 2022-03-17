namespace Kar.Banking.Domain.Entities;

public class Bank
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Account> Accounts { get; set; }
}
