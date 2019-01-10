using NUnit.Framework;

namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using VirtualCard.Business;
    using VirtualCard.Model;

    [TestFixture]
    public class AccountManagerTests
    {
        private List<Account> Accounts;
        private AccountManager AccountManager;
        private string TestAccountNumber = "xx-xx-1234";

        [SetUp]
        public void Setup()
        {
            this.Accounts =
                new List<Account>
                {
                    new Account
                    {
                        Number = this.TestAccountNumber,
                        Balance = 0,
                        Pin = 1234

                    }
                };

            this.AccountManager = new AccountManager(this.Accounts);
        }

        [Test]
        public void Purchase_WithCorrectPin_IsSuccessFull()
        {
            var account = this.Accounts.FirstOrDefault(x => x.Number == this.TestAccountNumber);
                
            account.Balance = 200m;

            //Execute
            var result = this.AccountManager.Purchase(this.TestAccountNumber, 120.00m, 1234);

            //Assertions
            result.Should().BeTrue();
            account.Balance.Should().Be(80m);
        }

        [Test]
        public void Purchase_WithCorrectPin_FailsNotEnoughFunds()
        {
            var account = this.Accounts.FirstOrDefault(x => x.Number == this.TestAccountNumber);

            account.Balance = 100m;

            //Execute
            var result = this.AccountManager.Purchase(this.TestAccountNumber, 120.00m, 1234);

            //Assertions
            result.Should().BeFalse();
            account.Balance.Should().Be(100m);
        }

        [Test]
        public void Purchase_WithInCorrectPin_Fails()
        {
            var account = this.Accounts.FirstOrDefault(x => x.Number == this.TestAccountNumber);
            account.Balance = 100m;

            //Execute
            var result = this.AccountManager.Purchase(this.TestAccountNumber, 80.00m, 4567);

            //Assertions
            result.Should().BeFalse();
            account.Balance.Should().Be(100m);
        }

        [Test]
        public void AddFunds()
        {
            var account = this.Accounts.FirstOrDefault(x => x.Number == this.TestAccountNumber);

            //Execute
            this.AccountManager.AddFunds(this.TestAccountNumber, 200.10m);
            this.AccountManager.AddFunds(this.TestAccountNumber, 0.90m);

            //Assertions
            account.Balance.Should().Be(201.0m);
        }

    }
}