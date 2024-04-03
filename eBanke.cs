// Developer: Nnachi, Joseph Otu
// Project: eBanke: Core Banking Software Application
// (Copyright (c)2024)



using System;
using System.Collections.Generic;

namespace CoreBankingSoftware
{
    public enum AccountType
    {
        QSavings, //Q-Savings Account = 10,000,000.
        Savings, //Savings Account = unlimited Amount
        FixedDeposit, //Capacity = Unlimited
        CurrentIndividual, //Capacity = Unlimited
        CurrentCorporate, //Capacity = Unlimited
        CurrentNonProfit //Capacity = Unlimited
    }

    public enum AccountStatus
    {
        New,
        Active,
        Inactive,
        UnderAudit,
        Frozen,
        Closed
    }

    public class Account
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string AccountNumber { get; private set; }
        public string AccountHolderName { get; set; }
        public decimal Balance { get; private set; }
        public AccountType Type { get; private set; }
        public AccountStatus Status { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime DateOfBirth { get; set; }

        public Account(string name, string address, AccountType type, DateTime dateOfBirth)
        {
            Name = name;
            Address = address;
            AccountNumber = GenerateAccountNumber();
            Balance = 0;
            Status = AccountStatus.Active;
            Type = type;
            CreationDate = DateTime.Now; // Set the creation date to current date and time
            DateOfBirth = dateOfBirth;
        }

        private string GenerateAccountNumber()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(10000000, 99999999); // Generate an 8-digit random number
            return $"ACCT{randomNumber}"; // Prefixing with "ACCT" for better readability
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
                return false;
            }
        }
    }

    public class CoreBankingSystem
    {
        private static List<Account> accounts = new List<Account>();
        private static int customerCount = 0;
        private const int MaxCustomers = 1000; // Assuming a maximum of 1000 customers

        public static void CreateAccount()
        {
            if (customerCount >= MaxCustomers)
            {
                Console.WriteLine("Sorry, maximum number of accounts has been reached. ");
                return;
            }

            Console.WriteLine("\nCreate Account:");
            Console.Write("     Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("     Enter your address: ");
            string address = Console.ReadLine();
            Console.Write("     Enter your date of birth and tap [ENTER] twice (YYYY-MM-DD): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("     Select Account Type: ");
            Console.WriteLine("  1. Q-Savings Accounts ");
            Console.WriteLine("  2. Savings Account ");
            Console.WriteLine("  3. Fixed Deposit Account ");
            Console.WriteLine("  4. Current Account [Corporate] ");
            Console.WriteLine("  5. Current Account [Individual] ");
            Console.WriteLine("  6. Current Account [Non-Profit] \n");
            Console.Write($"  Kindly enter any number from [1-6]  to select type of account: ");
            string accType = Console.ReadLine();
            AccountType type;
            switch (accType)
            {
                case "1":
                    type = AccountType.QSavings;
                    break;
                case "2":
                    type = AccountType.Savings;
                    break;
                case "3":
                    type = AccountType.FixedDeposit;
                    break;
                case "4":
                    type = AccountType.CurrentCorporate;
                    break;
                case "5":
                    type = AccountType.CurrentIndividual;
                    break;
                case "6":
                    type = AccountType.CurrentNonProfit;
                    break;
                default:
                    Console.WriteLine("Invalid account type. Account creation failed. ");
                    return;
            }
            // Generate a unique account number (you should implement this)
            int accountNumber = GenerateAccountNumber();
            // Create the account
            Account account = new Account(name, address, type, dateOfBirth);
            accounts.Add(account);
            customerCount++;
            Console.WriteLine();
            Console.WriteLine($" Account created successfully!  [Account Number: {account.AccountNumber} ], Name: {account.Name}, Type: {account.Type}, Balance: {account.Balance}, Creation Date: {account.CreationDate}");
        }

        private static int GenerateAccountNumber()
        {
            // This method should generate a unique account number
            // You can implement your logic here
            // For simplicity, let's just return a random number for now
            Random rand = new Random();
            return rand.Next(10000000, 99999999); // Return a 8-digit random number
        }

        public static void ViewAllAccounts()
        {
            Console.WriteLine("\nAll Accounts:");
            foreach (var account in accounts)
            {
                Console.WriteLine($"  [Account Number: {account.AccountNumber} ], Name: {account.Name}, Type: {account.Type}, Balance: {account.Balance}, Creation Date: {account.CreationDate} ");
            }
        }

        public static void ViewAccountByAccountNumber()
        {
            Console.Write("Enter Account Number: ");
            string accNumber = Console.ReadLine();
            var account = accounts.Find(acc => acc.AccountNumber == accNumber);
            if (account != null)
            {
                Console.WriteLine($"  [Account Number: {account.AccountNumber} ], Name: {account.Name}, Type: {account.Type}, Balance: {account.Balance}, Creation Date: {account.CreationDate}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // Frozen Accounts
        public static void ViewFrozenAccounts()
        {
            Console.WriteLine("\nFrozen Accounts:");
            foreach (var account in accounts)
            {
                if (account.Status == AccountStatus.Frozen)
                {
                    Console.WriteLine($"Account Number: {account.AccountNumber}, Name: {account.Name}, Type: {account.Type}, Balance: {account.Balance}");
                }
            }
        }

        public static void ViewAccountsWithBalance()
        {
            Console.WriteLine("\nAccounts with Balance:");
            foreach (var account in accounts)
            {
                if (account.Balance > 0)
                {
                    Console.WriteLine($"Account Number: {account.AccountNumber}, Name: {account.Name}, Type: {account.Type}, Balance: {account.Balance}");
                }
            }
        }

        public static void ViewEligibleAccountsForLoan()
        {
            // Implement logic to determine eligibility for a loan
            Console.WriteLine("\nEligible Accounts for Loan:");
            foreach (var account in accounts)
            {
                // Sample logic: Check balance, type, etc.
                if (account.Balance >= 1000 && account.Type == AccountType.Savings)
                {
                    Console.WriteLine($"Account Number: {account.AccountNumber}, Name: {account.Name}, Type: {account.Type}, Balance: {account.Balance}");
                }
                else
                {
                    Console.WriteLine(" No Account Found, [FUND] Account");
                }
            }
        }

        // FundsDeposit Class
        public static void DepositFunds()
        {
            Console.Write("Enter Account Number: ");
            string accNumber = Console.ReadLine();
            var account = accounts.Find(acc => acc.AccountNumber == accNumber);
            if (account != null)
            {
                Console.Write("Enter amount to deposit: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                account.Deposit(amount);
                Console.WriteLine($"Deposit successful. Updated Balance: {account.Balance}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // FundsWithdraw Class
        public static void WithdrawFunds()
        {
            Console.Write("Enter Account Number: ");
            string accNumber = Console.ReadLine();
            var account = accounts.Find(acc => acc.AccountNumber == accNumber);
            if (account != null)
            {
                Console.Write("Enter amount to withdraw: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                if (account.Withdraw(amount))
                {
                    Console.WriteLine($"Withdrawal successful. Updated Balance: {account.Balance}");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public static void TransferFunds()
        {
            Console.Write("Enter Sender's Account Number: ");
            string sourceAccNumber = Console.ReadLine();
            var sourceAccount = accounts.Find(acc => acc.AccountNumber == sourceAccNumber);
            if (sourceAccount != null)
            {
                Console.Write("Enter Recipient's Account Number: ");
                string destAccNumber = Console.ReadLine();
                var destAccount = accounts.Find(acc => acc.AccountNumber == destAccNumber);
                if (destAccount != null)
                {
                    Console.Write("Enter amount to transfer: ");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                    if (sourceAccount.Withdraw(amount))
                    {
                        destAccount.Deposit(amount);
                        Console.WriteLine($"Transfer successful. Updated Balance of Source Account: {sourceAccount.Balance}, Updated Balance of Destination Account: {destAccount.Balance}");
                    }
                    else
                    {
                        Console.WriteLine(" Transfer failed due to insufficient funds. ");
                    }
                }
                else
                {
                    Console.WriteLine("Recipient's Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Source Account not found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("             Welcome to eBanke!");
                Console.WriteLine("        Respond to the prompt then, press [ENTER] to continue ");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("          Please enter your name? ");
                string nom = Console.ReadLine();

                while (!done)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"    Hello {nom}! What would you want to do with eBanke today?");
                    Console.WriteLine();
                    Console.WriteLine("         1. Create Account");
                    Console.WriteLine("         2. View All Accounts");
                    Console.WriteLine("         3. View Account by Account Number");
                    Console.WriteLine("         4. View Frozen Accounts");
                    Console.WriteLine("         5. View Accounts with Balance");
                    Console.WriteLine("         6. View Eligible Accounts for Loan");
                    Console.WriteLine("         7. Deposit Funds");
                    Console.WriteLine("         8. Withdraw Funds");
                    Console.WriteLine("         9. Transfer Funds");
                    Console.WriteLine("         0. Exit \n");
                    Console.Write($"     {nom}, Kindly enter any number from [1-5] to try eBanke [0] to EXIT ebanke: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            CoreBankingSystem.CreateAccount();
                            break;
                        case "2":
                            CoreBankingSystem.ViewAllAccounts();
                            break;
                        case "3":
                            CoreBankingSystem.ViewAccountByAccountNumber();
                            break;
                        case "4":
                            CoreBankingSystem.ViewFrozenAccounts();
                            break;
                        case "5":
                            CoreBankingSystem.ViewAccountsWithBalance();
                            break;
                        case "6":
                            CoreBankingSystem.ViewEligibleAccountsForLoan();
                            break;
                        case "7":
                            CoreBankingSystem.DepositFunds();
                            break;
                        case "8":
                            CoreBankingSystem.WithdrawFunds();
                            break;
                        case "9":
                            CoreBankingSystem.TransferFunds();
                            break;
                        case "0":
                            Console.WriteLine("Exiting eBanke..");
                            done = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please try again.");
                            break;
                    }
                }
            }
        }
    }
}
