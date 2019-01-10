using System;

namespace VirtualCard.Model
{
    public class Account
    {
        public Account()
        {
            this.Balance = 0;
        }

        public Account(decimal balance)
        {
            Balance = balance;
        }

        public string Number { get; set; }
        public decimal Balance { get; set; }
        public int Pin { get; set; }
    }
}
