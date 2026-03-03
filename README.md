# Banking System


## Information
 - <p> Name : Abhijeet Shrestha </p>
 - Student ID / NP Number : NP071010
 - Course : <img><img width="30" height="30" alt="image" src="https://github.com/user-attachments/assets/b820f32e-82ea-4b6b-8082-68d79700c319" />
<img>

## Project Description
This is a console-based Banking System application built with C# and .NET. The system allows users to manage bank accounts with various account types, perform transactions, and maintain transaction history.

  

### Features

- **Multiple Account Types:**
  - **Savings Account**: Requires a minimum balance of 500
  - **Current Account**: Allows overdraft up to -1000
  - **Fixed Account**: Fixed deposit accounts with special rules

- **Account Operations:**
  - Create new accounts with customer information
  - Deposit money into accounts
  - Withdraw money with account-specific rules
  - Transfer money between accounts
  - Check account balance and customer details
  - View complete transaction history

- **Additional Features:**
  - Search for accounts by account number
  - Display all accounts in the system
  - Persistent data storage (accounts and transactions saved to file)
  - Transaction history tracking with timestamps
  - Exception handling for invalid operations

## How to Run

### Prerequisites

- .NET 7.0 or .NET 8.0 SDK installed on your system
- Windows, Linux, or macOS

### Running the Application

### Running in VS Code

1. **Install Extension:**
   - Open VS Code
   - Press Ctrl+Shift+X to open Extensions
   - Search for "C#" and install the C# extension by Microsoft

2. **Open Project:**
   - File → Open Folder → Select the project folder

3. **Run:**
   - Press Ctrl+` to open terminal
   - Type: `dotnet run`



### Running in Visual Studio

1. **Open Project:**
   - Launch Visual Studio
   - File → Open → Project/Solution
   - Select `final project.sln`

2. **Run:**
   - Click the green "Start" button (▶) or press F5
   - The console window will open with the application

3. **Build:**
   - Go to Build → Build Solution or press Ctrl+Shift+B



### Menu Options

Once the application starts, you'll see the following menu:

```
--- WELCOME TO BANK SYSTEM ---
1. Open New Account
2. Deposit Money
3. Withdraw Money
4. Transfer Money
5. Check Balance & Details
6. View Transaction History
7. Search Accounts
8. Show All Accounts
9. Exit
```

Simply enter the corresponding number to access each feature.



## Classes Overview

- **Customer**: Represents a bank customer with Name and Contact information.

- **Transaction**: Records transaction details including Type (Deposit/Withdrawal), Amount, Balance After transaction, and Date/Time.

- **Account (Abstract)**: Base class for all account types. Handles common operations like Deposit and maintains transaction history. Defines abstract Withdraw method for account-specific withdrawal rules.

- **SavingsAccount**: A savings account type that enforces a minimum balance of 500. Withdrawal is restricted if the balance would fall below this limit.

- **CurrentAccount**: A business account type that allows overdraft up to -1000. Useful for businesses requiring flexible credit limits.

- **FixedAccount**: A fixed deposit account where withdrawals are not permitted. Typically used for long-term savings with fixed interest rates.

- **Custom Exceptions**: 
  - **InsufficientBalanceException**: Thrown when an account balance is insufficient for a withdrawal.
  - **InvalidAccountException**: Thrown when invalid account operations are attempted.


## Data Store 

The system automatically saves all account and transaction data to `bank_data.txt` when you exit (Option 9). This data is loaded automatically when you restart the application.

## Technical Details

### Technologies Used
- **Language**: C#
- **Framework**: .NET 7.0 / .NET 8.0
- **Architecture**: Object-Oriented Programming with Abstraction, Inheritance and Polymorphism
- **Data Storage**: Text file-based 

### Project Structure
- `Models.cs`: Contains all class definitions (Account, Customer, Transaction, etc.)
- `Program.cs`: Main application logic and user interface
- `BankingSystem.csproj`: Project configuration file
- `bank_data.txt`: Data Store file (auto-generated)

## Account Rules

- **Savings Account**: Cannot withdraw if balance would fall below 500
- **Current Account**: Can have a maximum overdraft of -1000
- **All Accounts**: Cannot deposit negative or zero amounts
