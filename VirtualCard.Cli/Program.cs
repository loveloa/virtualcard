namespace VirtualCard
{
    using System;
    using System.Collections.Generic;
    using VirtualCard.Business;
    using VirtualCard.Model;

    class Program
    {
        private static bool exitapp = false;
        private static string accountNumber = "";

        private static List<Account> accounts = new List<Account>
        {
            new Account
            {
                Number = "abc-123",
                Balance = 20m,
                Pin = 1234
            }
        };
        private static AccountManager accountManager = new AccountManager(accounts);

        static void Main(string[] args)
        {
            Console.WriteLine("Available commands (add, purchase, account, exit)");

            while (!exitapp)
            {
                Console.Write("> ");
                var line = Console.ReadLine();

                if (line == "exit")
                {
                    exitapp = true;
                    continue;
                }

                if (line.StartsWith("account"))
                {
                    accountNumber = line.Split(' ')[1];
                    Console.WriteLine($"Further Transactions will be performed on account {accountNumber}");
                    continue;
                }

                if (line.StartsWith("add"))
                {
                    accountManager.AddFunds(accountNumber, Decimal.Parse(line.Split(' ')[1]));
                    Console.WriteLine($"Balance: {accountManager.GetBalance(accountNumber)}");
                    continue;
                }

                if (line.StartsWith("purchase"))
                {
                    if (accountManager.Purchase(
                        accountNumber,
                        Decimal.Parse(line.Split(' ')[1]),
                        Int32.Parse(line.Split(' ')[2])))
                    {
                        Console.WriteLine($"{accountNumber} Balance: {accountManager.GetBalance(accountNumber)}");
                    }
                    else
                    {
                        Console.WriteLine("Purchase failed");
                    }
                    continue;
                }

                Console.WriteLine("Unsupported command use (add, purchase, account, exit)");
            }
        }
    }
}
