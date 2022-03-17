using Kar.Banking.Application.Transactions.Transfer;
using Kar.Banking.Tests.Mocks;

namespace Kar.Banking.Tests.Commands;

public class TransferTests
{
    private readonly Mock<IAsyncRepository<Account>> _mockAccountRepository;
    private readonly TransferCommandHandler _handler;

    public TransferTests()
    {
        _mockAccountRepository = RepositoryMocks.GetAccountRepository();
        _handler = new TransferCommandHandler(_mockAccountRepository.Object);
    }

    [Fact]
    public async Task Handle_Valid_Transfer()
    {
        await _handler.Handle(new TransferCommand { SourceAccountId = 2, DestinationAccountId = 3, Amount = 5000 }, CancellationToken.None);

        var sourceAccount = await _mockAccountRepository.Object.GetByIdAsync(2);
        var destinationAccount = await _mockAccountRepository.Object.GetByIdAsync(3);

        sourceAccount.Balance.ShouldBe(1995000);
        destinationAccount.Balance.ShouldBe(55000);
    }

    [Fact]
    public async Task Exceed_Withdrawal_Limit()
    {
        var attemptWithdrawal = async () =>
        {
            await _handler.Handle(new TransferCommand { SourceAccountId = 1, DestinationAccountId = 2, Amount = 600 }, CancellationToken.None);
        };
        var exception = await Should.ThrowAsync<ValidationException>(() => attemptWithdrawal());
        exception.ValidationErrors.ShouldContain("Individual accounts have a withdrawal limit of 500 dollars.");
    }

    [Fact]
    public async Task WithdrawalAmount_Exceeds_Balance()
    {
        var attemptWithdrawal = async () =>
        {
            await _handler.Handle(new TransferCommand { SourceAccountId = 3, DestinationAccountId = 1, Amount = 50001 }, CancellationToken.None);
        };
        var exception = await Should.ThrowAsync<ValidationException>(() => attemptWithdrawal());
        exception.ValidationErrors.ShouldContain("Insufficient funds.");
    }
}
