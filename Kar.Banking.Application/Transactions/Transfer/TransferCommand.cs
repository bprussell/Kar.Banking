namespace Kar.Banking.Application.Transactions.Transfer;
public class TransferCommand : IRequest
{
    public int SourceAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
}
