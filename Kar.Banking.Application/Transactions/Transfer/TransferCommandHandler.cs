namespace Kar.Banking.Application.Transactions.Transfer;
public class TransferCommandHandler : IRequestHandler<TransferCommand>
{
    private readonly IAsyncRepository<Account> _accountRepository;

    public TransferCommandHandler(IAsyncRepository<Account> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Unit> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var sourceAccount = await _accountRepository.GetByIdAsync(request.SourceAccountId);
        var destinationAccount = await _accountRepository.GetByIdAsync(request.DestinationAccountId);

        var validator = new TransferCommandValidator(sourceAccount);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        sourceAccount.Balance -= request.Amount;
        destinationAccount.Balance += request.Amount;

        await _accountRepository.UpdateAsync(sourceAccount);
        await _accountRepository.UpdateAsync(destinationAccount);

        return Unit.Value;
    }
}
