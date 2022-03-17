using Kar.Banking.Application.Transactions.Deposit;
using Kar.Banking.Tests.Mocks;

namespace Kar.Banking.Tests.Commands;
public class DepositTests
{
    private readonly Mock<IAsyncRepository<Account>> _mockAccountRepository;

    public DepositTests()
    {
        _mockAccountRepository = RepositoryMocks.GetAccountRepository();
    }

    [Fact]
    public async Task Handle_ValidDeposit()
    {
        var handler = new DepositCommandHandler(_mockAccountRepository.Object);

        await handler.Handle(new DepositCommand { AccountId = 1, Amount = 2500 }, CancellationToken.None);

        var account = await _mockAccountRepository.Object.GetByIdAsync(1);
        account.Balance.ShouldBe(12500);
    }
}
