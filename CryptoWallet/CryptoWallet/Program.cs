using CryptoWallet.Classes;
using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;
using System.Collections;


static int UserInput(string typeOfInput)
{
    int input;
    do
    {
        Console.Write($"\nEnter {typeOfInput}: ");
    } while (!int.TryParse(Console.ReadLine(), out input));
    return input;
}


static bool UserConfirmation(string typeOfConfirmation)
{
    Console.Write($"\nAre you sure you want to {typeOfConfirmation}? (y/n) ");
    if (string.Compare(Console.ReadLine()!.ToLower(), "y") == 0)
    {
        return true;
    }
    return false;
}


static void PrintMenuOptions(Dictionary<string, List<string>> menuOptions, string menuToPrint)
{
    //Console.Clear();
    int i = 0;
    foreach (var item in menuOptions[menuToPrint])
    {
        Console.WriteLine($"{++i} - {item}");
    }
    // linija
}


static void MainMenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, Dictionary<string, Wallet> allWallets)
{
    while (true)
    {
        Console.Clear();
        PrintMenuOptions(menuOptions, "main menu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1: // create wallet
                CreateWalletSubmenu(menuOptions, fungibleAssetList, allWallets);
                break;

            case 2: // access wallet
                AccessWallet(menuOptions, allWallets, fungibleAssetList, nonFungibleAssetList);
                break;

            case 3:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}


static void CreateWalletSubmenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, Wallet> allWallets)
{
    while (true)
    {
        Console.Clear();
        PrintMenuOptions(menuOptions, "create wallet submenu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1: // create bitcoin wallet
                if (UserConfirmation("create a new bitcoin wallet?"))
                {
                    BitcoinWallet newWallet = new(fungibleAssetList);
                    allWallets.Add(newWallet.Address.ToString(), newWallet);
                }
                // press any key...
                return;

            case 2: // create ethereum wallet
                if (UserConfirmation("create a new ethereum wallet?"))
                {
                    EthereumWallet newWallet = new(fungibleAssetList);
                    allWallets.Add(newWallet.Address.ToString(), newWallet);
                }
                // press any key...
                return;

            case 3: // create solana wallet
                if (UserConfirmation("create a new solana wallet?"))
                {
                    SolanaWallet newWallet = new(fungibleAssetList);
                    allWallets.Add(newWallet.Address.ToString(), newWallet);
                }
                // press any key...
                return;

            case 4:
                return;

            default:
                break;
        }
    }
}


static void AccessWallet(Dictionary<string, List<string>> menuOptions, Dictionary<string, Wallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    while (true)
    {
        Console.WriteLine("Wallet address\t\t\t\tWallet type\tValue in USD\tValue change");
        // linija
        foreach (var wallet in allWallets)
        {
            Console.WriteLine(wallet.ToString());
        }
        // linija

        Console.Write("\nEnter the address of a wallet you want to access: ");
        string? walletAddress = Console.ReadLine();

        if (!allWallets.ContainsKey(walletAddress!))
        {
            continue;
        }

        PrintMenuOptions(menuOptions, "access wallet submenu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1:  // portfolio **********************************************************
                Portfolio(allWallets, walletAddress!, fungibleAssetList, nonFungibleAssetList);
                break;

            case 2:  // transfer  
                Transfer(allWallets, walletAddress!, fungibleAssetList, nonFungibleAssetList);
                break;

            case 3:  //  transaction history

                break;

            case 4:
                return;

            default:
                break;
        }
    }
}


static void Portfolio(Dictionary<string, Wallet> allWallets, string walletAddress, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    // ispisat adresu ime asseta i oznaku (fa), vrijednost, ukupna vrijednost u usd, postotak promjene
    Console.Clear();

    allWallets.TryGetValue(walletAddress, out Wallet? wallet);
    Dictionary<string, double> valueChanges = Class1.UpdateCryptocurrencyValues(fungibleAssetList, nonFungibleAssetList);
        

    Console.WriteLine($"Wallet address: {wallet!.Address}\n");

    foreach (var assetBalance in wallet.AssetBalances)
    {
        if (assetBalance.Value is 0)
        {
            continue;
        }
        string assetAddress = assetBalance.Key.ToString();
        //Console.WriteLine($"{fungibleAssetList[assetAddress].Name} [{fungibleAssetList[assetAddress].Abbreviation}] - {assetBalance.Value}");
        // key not found --

    }

    Console.ReadKey();    
}


static string AddressInput(Dictionary<string, Wallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, string typeOfAddress, string message)
{
    Console.Write($"\nEnter the address of {message}: ");
    switch (typeOfAddress)
    {
        case "wallet":
            while (true)
            {
                string? walletAddress = Console.ReadLine();

                if (allWallets.ContainsKey(walletAddress!))
                {
                    return walletAddress!;
                }
            }

        case "fungibleAsset":
            // ---


        case "nonFungibleAsset":
            // ---


        default:
            return "";
    }
}


static void Transfer(Dictionary<string, Wallet> allWallets, string SenderWalletAddress, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    Console.Clear();

    string receiverWalletAddress = AddressInput(allWallets, fungibleAssetList, nonFungibleAssetList, "wallet", "to receieve the transfer");
    string? assetAddressString;
    Guid assetAddress;

    while (true)
    {
        Console.Write("\nEnter the address of an asset you want to transfer: ");
        assetAddressString = Console.ReadLine();

        if (fungibleAssetList.ContainsKey(assetAddressString!))
        {
            Guid.TryParse(assetAddressString, out assetAddress);
            break;
        }
        else if (!nonFungibleAssetList.ContainsKey(assetAddressString!))
        {
            continue;
        }

        // NonFungibleAssetTransaction nemogu doc do nje priko allWallets
    }

    double amount;
    while (true)
    {
        Console.Write("\nEnter the amount you want to transfer: ");

        if (double.TryParse(Console.ReadLine(), out amount))
        {
            break;
        }
    }

    if (allWallets[SenderWalletAddress].CreateNewFungibleAssetTransactionRecord(allWallets[receiverWalletAddress], assetAddress, amount))
    {
        Console.WriteLine("Succesfull\nPress any key to continue.");
        Console.ReadKey();
    }
}


static void SeedData(Dictionary<string, Wallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    fungibleAssetList.Add("bitcoin", new FungibleAsset("bitcoin", "BTC", 16989.53));
    fungibleAssetList.Add("xrp", new FungibleAsset("xrp", "XRP", 0.39));
    fungibleAssetList.Add("dogecoin", new FungibleAsset("dogecoin", "DOGE", 0.1));
    fungibleAssetList.Add("ethereum", new FungibleAsset("ethereum", "ETH", 1273.8));
    fungibleAssetList.Add("polygon", new FungibleAsset("polygon", "MATIC", 0.92));
    fungibleAssetList.Add("tether", new FungibleAsset("tether", "USDT", 1));
    fungibleAssetList.Add("solana", new FungibleAsset("solana", "SOL", 13.57));
    fungibleAssetList.Add("shibainu", new FungibleAsset("shiba inu", "SHIB", 0.00000929));
    fungibleAssetList.Add("bnb", new FungibleAsset("bnb", "BNB", 290.68));
    fungibleAssetList.Add("cosmos", new FungibleAsset("comsos", "ATOM", 10.22));

    BitcoinWallet bitcoinWallet1 = new(fungibleAssetList);
    BitcoinWallet bitcoinWallet2 = new(fungibleAssetList);
    BitcoinWallet bitcoinWallet3 = new(fungibleAssetList);

    allWallets.Add(bitcoinWallet1.Address.ToString(), bitcoinWallet1);
    allWallets.Add(bitcoinWallet2.Address.ToString(), bitcoinWallet2);
    allWallets.Add(bitcoinWallet3.Address.ToString(), bitcoinWallet3);

    nonFungibleAssetList.Add("Moonbirds#1748", new NonFungibleAsset("Moonbirds#1748", 8.74, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("TerraformsLevel13", new NonFungibleAsset("TerraformsLevel13", 0.48, fungibleAssetList["ethereum"].Address, "ethereum"));
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
    //nonFungibleAssetList.Add();
}

Dictionary<string, List<string>> menuOptions = new()
            {
                {"main menu", new List<string>(){ "Create wallet", "Access wallet", "Exit application"} },
                {"create wallet submenu", new List<string>() { "Bitcoin wallet", "Ethereum wallet", "Solana wallet", "Return to main menu"} },
                {"access wallet submenu", new List<string>() { "Portfolio", "Transfer", "Transaction history", "Return to main menu" } }
            };

Dictionary<string, Wallet> allWallets = new(); 

Dictionary<string, FungibleAsset> fungibleAssetList = new();
Dictionary<string, NonFungibleAsset> nonFungibleAssetList = new();

SeedData(allWallets, fungibleAssetList, nonFungibleAssetList);
MainMenu(menuOptions, fungibleAssetList, nonFungibleAssetList, allWallets);