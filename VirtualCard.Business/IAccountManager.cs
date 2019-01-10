namespace VirtualCard.Business
{
    using System;

    public interface IAccountManager
    {
        bool AddFunds(string accountNumber, decimal amount);

        bool Purchase(string accountNumber, decimal amount, int pin);

        Decimal GetBalance(string accountNumber);
    }
}