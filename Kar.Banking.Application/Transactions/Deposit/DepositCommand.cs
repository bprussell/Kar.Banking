namespace Kar.Banking.Application.Transactions.Deposit;
public class DepositCommand : IRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
}
