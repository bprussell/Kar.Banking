namespace Kar.Banking.Application.Transactions.Withdraw;
public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand>
{
    private readonly IAsyncRepository<Account> _accountRepository;

    public WithdrawCommandHandler(IAsyncRepository<Account> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var withdrawAccount = await _accountRepository.GetByIdAsync(request.AccountId);

        var validator = new WithdrawCommandValidator(withdrawAccount);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        withdrawAccount.Balance -= request.Amount;

        await _accountRepository.UpdateAsync(withdrawAccount);

        return Unit.Value;
    }
}
