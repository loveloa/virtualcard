namespace VirtualCard.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VirtualCard.Model;

    public class AccountManager : IAccountManager
    {
        private readonly List<Account> accounts;

        public AccountManager(List<Account> accounts)
        {
            this.accounts = accounts;
        }

        public bool AddFunds(string accountNumber, decimal amount)
        {
            var account = this.accounts.FirstOrDefault(x => x.Number == accountNumber);
            if (account != null)
            {
                lock (account)
                {
                    account.Balance += amount;
                    return true;
                }
            }

            return false;
        }

        public bool Purchase(string accountNumber, decimal amount, int pin)
        {
            var account = this.accounts.FirstOrDefault(x => x.Number == accountNumber);

            if (account != null)
            {
                lock (account)
                {
                    if (account.Balance >= amount && account.Pin == pin)
                    {
                        account.Balance -= amount;
                        return true;
                    }
                }
            }

            return false;
        }

        public Decimal GetBalance(string accountNumber)
        {
            var account = this.accounts.FirstOrDefault(x => x.Number == accountNumber);

            if (account != null)
            {
                lock (account)
                {
                    return account.Balance;
                }
            }

            return 0;
        }
    }
}