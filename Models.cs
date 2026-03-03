using System;
using System.Collections.Generic;

namespace BankingSystem
{
    // Error handling classes 
    public class InsufficientBalanceException : Exception 
    { 
        public InsufficientBalanceException(string msg) : base(msg) { } // base(msg) send the message to the parent 
    }

    public class InvalidAccountException : Exception 
    { 
        public InvalidAccountException(string msg) : base(msg) { } 
    }

    // Customer Class
    public class Customer
    {
        public string Name = string.Empty;
        public string Contact = string.Empty;
    }

    // Transaction Class
    public class Transaction
    {
        public string Type = string.Empty;
        public double Amount;
        public double BalanceAfter;
        public string Date = string.Empty;

        public string ToTextLine() // for file saving
        {
            return $"{Date}|{Type}|{Amount}|{BalanceAfter}";
        }
    }

    //  Account Class 
    public abstract class Account
    {
        public string AccountNumber = string.Empty;
        public Customer Owner = new Customer();
        public double Balance;
        
        // create a empty list to store history data
        public List<Transaction> History = new List<Transaction>(); 

        // Method to record a transaction
        public void AddToHistory(string type, double amount)
        {
            Transaction t = new Transaction();
            t.Type = type;
            t.Amount = amount;
            t.BalanceAfter = this.Balance;
            t.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            History.Add(t);
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("you must  deposit a positive number.");
                return; // 
            }
            Balance += amount;
            AddToHistory("Deposit", amount);
            Console.WriteLine($"Successfully deposited {amount}. New balance: {Balance}");
        }

        // Abstract method - each account type decides its own withdrawal rules (Polymorphism)
        public abstract void Withdraw(double amount);
    }

    // Savings Account 
    public class SavingsAccount : Account
    {
        public override void Withdraw(double amount)
        {
            if (Balance - amount < 500) // you a/c have must keep 500
            {
                throw new InsufficientBalanceException("Savings accounts must keep a minimum balance of 500.");
            }
            Balance -= amount;
            AddToHistory("Withdrawal", -amount);
            Console.WriteLine($"Successfully withdrew {amount}. New balance: {Balance}");
        }
    }

    // Current Account 
    public class CurrentAccount : Account
    {
        public override void Withdraw(double amount)
        {
            if (Balance - amount < -1000) // you can go up to -1000 in current account
            {
                throw new InsufficientBalanceException("Current account withdraw limit is -1000.");
            }
            Balance -= amount;
            AddToHistory("Withdrawal", -amount);
            Console.WriteLine($"Successfully withdrew {amount}. New balance: {Balance}");
        }
    }

    // Fixed Deposit Account
    public class FixedAccount : Account
    {
        public override void Withdraw(double amount)
        {
            
            throw new InvalidOperationException("Withdrawals are not allowed from a Fixed Deposit account.");
        }
    }
}
