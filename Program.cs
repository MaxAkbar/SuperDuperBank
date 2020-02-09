using System;
using System.IO;

namespace SuperDuperBank
{
    class Program
    {
        static void Main(string[] args)
        {
            // ** Write a banking system application that allows to make deposits and withdraws
            Console.WriteLine("Welcome to Super Duper Bank.");

            // product requirements
            // 1.  Allow users to log into the system using their name
            // 1.1 Ask the user's name
            Console.WriteLine("Please provide your name to access your account.");

            // 1.2 remember the user's name
            var userName = Console.ReadLine();
            var userBalance = 0;


            // 1.3 read a file by the user's name (the file should be csv format)
            // 1.4 if the file doesn't exists keep the user's balance at 0
            if (File.Exists($"{userName}.csv"))
            {
                // 1.5 if the file exists then read the last line and remember the user's balance
                string[] bankAccount = File.ReadAllLines($"{userName}.csv");
                string[] userLedger = bankAccount[^1].Split(',');

                int.TryParse(userLedger[^1], out userBalance);
            }

            Console.WriteLine($"Your current balance is {userBalance}");

            // 2.  Ask the user if they like to deposit or withdraw
            Console.WriteLine("Please enter 1 to withdraw or 2 to deposit.");
            var userResponse = Console.ReadLine();

            if (userResponse == "1")
            {
                // 2.1  if withdraw then ask how much
                Console.WriteLine("How much would you like to withdraw.");
                
                var userWithdrawAmount = Console.ReadLine();

                int.TryParse(userWithdrawAmount, out var withDrawAmount);

                // 2.2 then check the balance and see if the user has enough money
                if (withDrawAmount < userBalance)
                {
                    // 2.3 if the user has enough money then allow the withdraw and decrement the balance and record the line to the user's files
                    var newBalance = userBalance - withDrawAmount;

                    File.AppendAllText($"{userName}.csv", $"{DateTime.Now},withdraw,{withDrawAmount},{newBalance}{Environment.NewLine}");
                    Console.WriteLine($"You have withdrawn {withDrawAmount} from your account.");
                    Console.WriteLine($"Your new balance is {newBalance}.");
                }
                else
                {
                    // 2.4 if the user does not have enough then tell them they can't withdraw
                    Console.WriteLine($"We are sorry but you can make a withdraw greater than your current balance {userBalance}.");
                }
            }

            if (userResponse == "2")
            {
                // 3.  if the user selected to make a deposit then ask the user how much to deposit
                Console.WriteLine("How much would you like to deposit?");
                
                var userDepositAmount = Console.ReadLine();

                int.TryParse(userDepositAmount, out var depositAmount);

                // 3.1 then log the data into the user's file
                File.AppendAllText($"{userName}.csv", $"{DateTime.Now},deposit,{depositAmount},{userBalance + depositAmount}{Environment.NewLine}");

                Console.WriteLine($"Thank you for your deposit. Your new balance is {userBalance + depositAmount}");
            }
        }
    }
}
