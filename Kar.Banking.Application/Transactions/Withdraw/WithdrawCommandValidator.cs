namespace Kar.Banking.Application.Transactions.Withdraw;
public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
{
    private readonly Account _account;
    public WithdrawCommandValidator(Account account)
    {
        _account = account;

        RuleFor(x => x)
            .Must(ValidateWithdrawalLimit)
            .WithMessage("Individual accounts have a withdrawal limit of 500 dollars.");

        RuleFor(x => x.Amount)
            .LessThanOrEqualTo(_account.Balance)
            .WithMessage("Insufficient funds.");
    }

    private bool ValidateWithdrawalLimit(WithdrawCommand command)
    {
        if (_account is InvestmentAccount && ((InvestmentAccount)_account).AccountType == AccountTypes.Individual)
        {
            if(command.Amount > 500)
                return false;
        }

        return true;
    }
}
