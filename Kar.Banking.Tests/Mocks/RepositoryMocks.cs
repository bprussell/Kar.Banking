namespace Kar.Banking.Tests.Mocks;
public class RepositoryMocks
{
    public static Mock<IAsyncRepository<Account>> GetAccountRepository()
    {
        var bank = new Bank
        {
            Id = 1,
            Name = "Chase",
            Accounts = new List<Account>
            {
                new InvestmentAccount
                {
                    Id = 1,
                    AccountType = AccountTypes.Individual,
                    Balance = 10000,
                    Owner = "Joe Lunchpail"
                },
                new InvestmentAccount
                {
                    Id = 2,
                    AccountType = AccountTypes.Corporate,
                    Balance = 2000000,
                    Owner = "Initech"
                },
                new CheckingAccount
                {
                    Id = 3,
                    Balance = 50000,
                    Owner = "Caroline Davies"
                }
            }
        };

        var mockAccountRepository = new Mock<IAsyncRepository<Account>>();
        mockAccountRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
            (int accountId) =>
            {
                return bank.Accounts.Single(a => a.Id == accountId);
            });

        mockAccountRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Account>())).Callback<Account>(
            (Account updatedAccount) =>
            {
                var account = bank.Accounts.Single(a => a.Id == updatedAccount.Id);
                account.Balance = updatedAccount.Balance;
            });

        return mockAccountRepository;
    }
}
