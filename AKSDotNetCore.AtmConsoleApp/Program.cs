using System;

public class cardHolder
{
    string cardNum;
    int pin;
    string firstName;
    string lastName;
    double balance;

    public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    public string getNum()
    {
        return cardNum;
    }
    public int getPin()
    {
        return pin;
    }
    public string getFirstName()
    {
        return firstName;
    }
    public string getLastName()
    {
        return lastName;
    }
    public double getBalance()
    {
        return balance;
    }

    public void setNum(string newCardNum) 
    {
        cardNum = newCardNum;
    }
    public void setPin(int newPin)
    {
        pin = newPin;
    }
    public void setFirstName(string newFirstName)
    {
        firstName = newFirstName;
    }
    public void setLastName(string newLastName)
    {
        lastName = newLastName;
    }
    public void setBalance(double newBalance)
    {
        balance = newBalance;
    }

    public static void Main(string[] args)
    {
        void printOptions()
        {
            Console.WriteLine("Please choose one of the following options :");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        void deposit(cardHolder currentUser)
        {
            Console.WriteLine("How much would you like to deposit :");
            double deposit = Convert.ToDouble(Console.ReadLine());
            currentUser.setBalance(deposit);
            Console.WriteLine($"Your balance is : {currentUser.getBalance()}");
        }

        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much would you like to withdraw :");
            double withdraw = Convert.ToDouble(Console.ReadLine());
            if (currentUser.getBalance() < withdraw)
            {
                Console.WriteLine("Insufficient Balance!");
            }
            else
            {
                currentUser.setBalance(currentUser.getBalance() - withdraw);
                Console.WriteLine($"Your remaining balance is : {currentUser.getBalance()}");
            }
        }

        void balance (cardHolder currentUser)
        {
            Console.WriteLine($"Current Balance : {currentUser.getBalance()}");
        }

        List<cardHolder> cardHolders = new List<cardHolder>();
        cardHolders.Add(new cardHolder("123456789", 1111, "AA", "S", 350.00));
        cardHolders.Add(new cardHolder("123459876", 2222, "BB", "S", 250.00));
        cardHolders.Add(new cardHolder("987654321", 3333, "CC", "S", 550.00));
        cardHolders.Add(new cardHolder("987651234", 4444, "DD", "S", 150.00));
        cardHolders.Add(new cardHolder("012345678", 5555, "EE", "S", 100.00));

        //Promt User
        Console.WriteLine("Welcome ATM Console");
        Console.WriteLine("Please insert your debit card : ");
        string debitCardNum = "";
        cardHolder currentUser;

        while (true)
        {
            try
            {
                debitCardNum = Console.ReadLine();
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardNum);
                if (currentUser != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Card not recognized. Please try again");
                }
            }
            catch
            {
                Console.WriteLine("Card not recognized. Please try again");
            }
        }

        Console.WriteLine("Please enter your pin :");
        int userPin = 0;
        while (true)
        {
            try
            {
                userPin = Convert.ToInt32(Console.ReadLine());
                currentUser = cardHolders.FirstOrDefault(a => a.pin == userPin);
                if (currentUser != null) // if (currentUser.Pin() == userPin)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect Password. Please try again");
                }
            }
            catch
            {
                Console.WriteLine("Incorrect Password. Please try again");
            }
        }

        Console.WriteLine($"Welcome {currentUser.getFirstName()}");
        int option = 0;

        do
        {
            printOptions();
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                throw new Exception("Choose one option");
            }
            switch(option)
            {
                case 1:
                    deposit(currentUser); break;
                case 2:
                    withdraw(currentUser); break;   
                case 3:
                    balance(currentUser); break;    
                case 4:
                    break;
            }
        }
        while (option != 4);
        Console.WriteLine("Thank for using");
        Console.ReadKey();
    }
}