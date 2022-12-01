using System.Collections.Generic;
using System;

namespace CryptoWallet
{
    internal class Program
    {
        static int UserInput(string typeOfInput)
        {
            int input;
            while (int.TryParse(Console.ReadLine(), out input))
            {
                Console.Write($"Enter {typeOfInput}: ");
            }
            return input;
        }


        static void MainMenu(Dictionary<string, List<string>> menuOptions)
        {
            while (true)
            {
                Console.Clear();
                int i = 0;
                foreach (var item in menuOptions["main menu"])
                {
                    Console.WriteLine($"{++i} - {item}\n");
                }
                // linija 

                switch (UserInput("a number to navigate the menu"))
                {
                    case 1: // create wallet

                        break;

                    case 2: // access wallet

                        break;

                    case 3:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }


        static void CreateWallet(Dictionary<string, List<string>> menuOptions)
        {
            while (true)
            {
                Console.Clear();
                int i = 0;
                foreach (var item in menuOptions["create wallet submenu"])
                {
                    Console.WriteLine($"{++i} - {item}\n");
                }
                // linija

                switch (UserInput("a number to navigate the menu"))
                {
                    case 1: // create bitcoin wallet

                        break;

                    case 2: // create ethereum wallet

                        break;

                    case 3: // create solana wallet

                        break;

                    case 4: 
                        return;

                    default:
                        break;
                }
            }
        }


        static void AccessWallet()
        {

        }


        static void Main(string[] args)
        {
            Dictionary<string, List<string>> menuOptions = new Dictionary<string, List<string>>()
            {
                {"main menu", new List<string>(){ "Create wallet", "Access wallet", "Exit application"} },
                {"create wallet submenu", new List<string>() { "Bitcoin wallet", "Ethereum wallet", "Solana wallet", "Return to main menu"} },
                {"access wallet submenu", new List<string>() { "Portfolio", "Transfer", "Transaction history", "Return to main menu" } }
            };

            

        }
    }
}
