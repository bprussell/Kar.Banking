namespace Kar.Banking.Application.Transactions.Deposit;
public class DepositCommandHandler : IRequestHandler<DepositCommand>
{
    private readonly IAsyncRepository<Account> _accountRepository;

    public DepositCommandHandler(IAsyncRepository<Account> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var depositAccount = await _accountRepository.GetByIdAsync(request.AccountId);

        depositAccount.Balance += request.Amount;

        await _accountRepository.UpdateAsync(depositAccount);

        return Unit.Value;
    }
}
