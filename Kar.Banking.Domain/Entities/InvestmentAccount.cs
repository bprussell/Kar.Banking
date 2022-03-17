using Kar.Banking.Domain.Enums;

namespace Kar.Banking.Domain.Entities;
public class InvestmentAccount : Account
{
    public AccountTypes AccountType { get; set; }
}
