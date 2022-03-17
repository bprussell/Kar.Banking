namespace Kar.Banking.Application.Transactions.Withdraw;
public class WithdrawCommand : IRequest
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
}
