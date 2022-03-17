namespace Kar.Banking.Application.Transactions.Transfer;
public class TransferCommandValidator : AbstractValidator<TransferCommand>
{
    private readonly Account _sourceAccount;
    public TransferCommandValidator(Account sourceAccount)
    {
        _sourceAccount = sourceAccount;

        RuleFor(x => x)
            .Must(ValidateWithdrawalLimit)
            .WithMessage("Individual accounts have a withdrawal limit of 500 dollars.");

        RuleFor(x => x.Amount)
            .LessThanOrEqualTo(x => _sourceAccount.Balance)
            .WithMessage("Insufficient funds.");
    }

    private bool ValidateWithdrawalLimit(TransferCommand command)
    {
        if (_sourceAccount is InvestmentAccount && ((InvestmentAccount)_sourceAccount).AccountType == AccountTypes.Individual)
        {
            if (command.Amount > 500)
                return false;
        }

        return true;
    }
}
