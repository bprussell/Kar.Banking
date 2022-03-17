using Kar.Banking.Application.Transactions.Withdraw;
using Kar.Banking.Tests.Mocks;

namespace Kar.Banking.Tests.Commands;
public class WithdrawalTests
{
    private readonly Mock<IAsyncRepository<Account>> _mockAccountRepository;
    private readonly WithdrawCommandHandler _handler;

    public WithdrawalTests()
    {
        _mockAccountRepository = RepositoryMocks.GetAccountRepository();
        _handler = new WithdrawCommandHandler(_mockAccountRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidWithdrawal()
    {
        await _handler.Handle(new WithdrawCommand { AccountId = 2, Amount = 30000 }, CancellationToken.None);

        var account = await _mockAccountRepository.Object.GetByIdAsync(2);
        account.Balance.ShouldBe(1970000);
    }

    [Fact]
    public async Task Exceed_Withdrawal_Limit()
    {
        var attemptWithdrawal = async () =>
        {
            await _handler.Handle(new WithdrawCommand { AccountId = 1, Amount = 600 }, CancellationToken.None);
        };
        var exception = await Should.ThrowAsync<ValidationException>(() => attemptWithdrawal());
        exception.ValidationErrors.ShouldContain("Individual accounts have a withdrawal limit of 500 dollars.");
    }

    [Fact]
    public async Task WithdrawalAmount_Exceeds_Balance()
    {
        var attemptWithdrawal = async () =>
        {
            await _handler.Handle(new WithdrawCommand { AccountId = 3, Amount = 50001 }, CancellationToken.None);
        };
        var exception = await Should.ThrowAsync<ValidationException>(() => attemptWithdrawal());
        exception.ValidationErrors.ShouldContain("Insufficient funds.");
    }
}
