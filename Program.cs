using System;
using System.Collections.Generic; 
using System.IO;

namespace BankingSystem
{
    class Program
    {
        // This list will store all data of our accounts while the program runs
        static List<Account> allAccounts = new List<Account>();
        static string fileName = "bank_data.txt";

        static void Main(string[] args)
        {
            LoadFromFile(); // Load any saved data at the start

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- WELCOME TO BANK SYSTEM ---");
                Console.WriteLine("1. Open New Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Transfer Money");
                Console.WriteLine("5. Check Balance & Details");
                Console.WriteLine("6. View Transaction History");
                Console.WriteLine("7. Search Accounts");
                Console.WriteLine("8. Show All Accounts");
                Console.WriteLine("9. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine() ?? string.Empty;

                try
                {
                    switch (choice)
                    {
                        case "1": CreateNewAccount(); break;
                        case "2": Deposit(); break;
                        case "3": Withdraw(); break;
                        case "4": Transfer(); break;
                        case "5": CheckBalance(); break;
                        case "6": ViewHistory(); break;
                        case "7": Search(); break;
                        case "8": ShowAll(); break;
                        case "9": SaveToFile(); running = false; break;
                        default: Console.WriteLine("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

     
         
        static void CreateNewAccount()
        {
            Console.Write("Enter your Name: ");
            string name = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter your Contact: ");
            string contact = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Type: 1. Savings  2. Current  3.Fixed" );
            string type = Console.ReadLine() ?? string.Empty;
            
            
            Account newAcc;
            if (type == "1") newAcc = new SavingsAccount();
            else if (type == "2") newAcc = new CurrentAccount();
            else newAcc = new FixedAccount();

            newAcc.AccountNumber = "ACC" + (allAccounts.Count + 101);
            newAcc.Owner.Name = name;
            newAcc.Owner.Contact = contact;
            newAcc.Balance = 0;

            allAccounts.Add(newAcc);
            Console.WriteLine("Account Created Successfully and your acc number is " + newAcc.AccountNumber);
        }

        static Account FindAccount(string number)
        {
            foreach (Account acc in allAccounts)
            {
                if (acc.AccountNumber == number) return acc;
            }
            throw new InvalidAccountException("Account is not found!");
        }

        static void Deposit()
        {
            Console.Write("Enter your account number: ");
            string num = Console.ReadLine() ?? string.Empty;
            Account acc = FindAccount(num);
            Console.Write("Amount to Deposit: ");
            double amount = double.Parse(Console.ReadLine() ?? "0");
            acc.Deposit(amount);
        }

        static void Withdraw()
        {
            Console.Write("Account Number: ");
            string num = Console.ReadLine() ?? string.Empty;
            Account acc = FindAccount(num);
            if (acc is FixedAccount)
            {
                Console.WriteLine("You cannot withdraw money from a Fixed Deposit account.");
            }

            else
            {
                
            Console.Write("Enter a amount you want  to Withdraw: ");
            double amount = double.Parse(Console.ReadLine() ?? "0");
            acc.Withdraw(amount);
            }
        }

        static void Transfer()
        {
            Console.Write("Enter a sender account: ");
            string from = Console.ReadLine() ?? string.Empty;
            Console.Write("enter a receiver account: ");
            string to = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter a amount you want transfer: ");
            double amount = double.Parse(Console.ReadLine() ?? "0");

            Account sender = FindAccount(from);
            Account receiver = FindAccount(to);

            sender.Withdraw(amount);
            receiver.Deposit(amount);
            Console.WriteLine("Transfer complete!");
        }

        static void CheckBalance()
        {
            Console.Write("Enter a account number: ");
            string num = Console.ReadLine() ?? string.Empty;
            Account acc = FindAccount(num);
            Console.WriteLine("\n--- DETAILS ---");
            Console.WriteLine("Name: " + acc.Owner.Name);
            Console.WriteLine("Type: " + acc.GetType().Name);
            Console.WriteLine("Balance: " + acc.Balance);
        }

        static void ViewHistory()
        {
            Console.Write("Enter a  account number: ");
            string num = Console.ReadLine() ?? string.Empty;
            Account acc = FindAccount(num);
            Console.WriteLine("\n--- HISTORY ---");
            foreach (Transaction t in acc.History)
            {
                Console.WriteLine($"{t.Date} | {t.Type} | {t.Amount} |  Balance: {t.BalanceAfter}");
            }
        }

        static void Search()
        {
            Console.Write("Enter Name to Search: ");
            string name = Console.ReadLine() ?? string.Empty;
            foreach (Account acc in allAccounts)
            {
                if (acc.Owner.Name.ToLower().Contains(name.ToLower()))
                {
                    Console.WriteLine($"Found: {acc.AccountNumber} - {acc.Owner.Name}");
                }
            }
        }

        static void ShowAll()
        {
            Console.WriteLine("\n--- ALL ACCOUNTS ---");
            foreach (Account acc in allAccounts)
            {
                Console.WriteLine($"Account number: {acc.AccountNumber} | Name: {acc.Owner.Name} | Balance: {acc.Balance} | Account Type:{acc.GetType().Name}");
            }
        }

        // File Saving/Loading 

        static void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Account acc in allAccounts)
                {
                    // Format: Type,Number,Name,Contact,Balance
                    string type = "Current";
                    if (acc is SavingsAccount) type = "Savings";
                    else if (acc is FixedAccount) type = "Fixed";
                    
                    writer.WriteLine($"{type} | {acc.AccountNumber}| {acc.Owner.Name} | {acc.Owner.Contact} | {acc.Balance} ");
                }
            }
            Console.WriteLine("Data saved to file.");
        }

        static void LoadFromFile()
        {
            if (!File.Exists(fileName)) return;

            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                string[] parts = line.Split(',');
                if (parts.Length < 5) continue;

                Account acc;
                if (parts[0] == "Savings") acc = new SavingsAccount();
                else if (parts[0] == "Fixed") acc = new FixedAccount();
                else acc = new CurrentAccount();

                acc.AccountNumber = parts[1];
                acc.Owner.Name = parts[2];
                acc.Owner.Contact = parts[3];
                acc.Balance = double.Parse(parts[4]);

                allAccounts.Add(acc);
            }
        }
    }
}
