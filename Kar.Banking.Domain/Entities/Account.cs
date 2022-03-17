using Kar.Banking.Domain.Enums;

namespace Kar.Banking.Domain.Entities;
public class Account
{
    public int Id { get; set; }
    public string Owner { get; set; }
    public decimal Balance { get; set; }
}
