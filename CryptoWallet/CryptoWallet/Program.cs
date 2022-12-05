using CryptoWallet.Classes;
using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;
using System.Collections;
using System.Reflection;


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


static void ResultOfAction(string message)
{
    Console.WriteLine($"\n{message}.\nPress any key to continue.");
    Console.ReadKey();
}


static void PrintMenuOptions(Dictionary<string, List<string>> menuOptions, string menuToPrint)
{
    int i = 0;
    foreach (var item in menuOptions[menuToPrint])
    {
        Console.WriteLine($"{++i} - {item}");
    }
    // linija
}


static void MainMenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, Dictionary<string, IWallet> allWallets)
{
    while (true)
    {
        Console.Clear();
        PrintMenuOptions(menuOptions, "main menu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1: // create wallet
                Console.Clear();
                CreateWalletSubmenu(menuOptions, fungibleAssetList, nonFungibleAssetList, allWallets);
                break;

            case 2: // access wallet
                Console.Clear();
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


static void CreateWalletSubmenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, Dictionary<string, IWallet> allWallets)
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
                return;

            case 2: // create ethereum wallet
                if (UserConfirmation("create a new ethereum wallet?"))
                {
                    EthereumWallet newWallet = new(fungibleAssetList, nonFungibleAssetList);
                    allWallets.Add(newWallet.Address.ToString(), newWallet);
                }
                return;

            case 3: // create solana wallet
                if (UserConfirmation("create a new solana wallet?"))
                {
                    SolanaWallet newWallet = new(fungibleAssetList, nonFungibleAssetList);
                    allWallets.Add(newWallet.Address.ToString(), newWallet);
                }
                return;

            case 4:
                return;

            default:
                break;
        }
    }
}


static void AccessWallet(Dictionary<string, List<string>> menuOptions, Dictionary<string, IWallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Wallet address\t\t\t\tWallet type");
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

        Console.Clear();
        PrintMenuOptions(menuOptions, "access wallet submenu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1:  // portfolio 
                Portfolio(allWallets, walletAddress!, fungibleAssetList, nonFungibleAssetList);
                return;

            case 2:  // transfer  
                Transfer(allWallets, walletAddress!, fungibleAssetList, nonFungibleAssetList);
                return;

            case 3:  //  transaction history
                TransactionHistory(allWallets, walletAddress!);
                return;

            case 4: // main menu
                return;

            default:
                break;
        }
    }
}


static void Portfolio(Dictionary<string, IWallet> allWallets, string walletAddress, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    Console.Clear();

    allWallets.TryGetValue(walletAddress, out IWallet? wallet);

    // neradi metoda za izracunavanje ukupne vrijednosti 
    Dictionary<string, double> valueChanges = HelperClass.UpdateCryptocurrencyValues(fungibleAssetList, nonFungibleAssetList);
        
    Console.WriteLine($"Total value in USD: {wallet!.TotalValueInUSD} \n\nBalances:");

    foreach (var assetBalance in wallet.AssetBalances)
    {
        if (assetBalance.Value is 0)
        {
            continue;
        }
        Console.WriteLine($"{HelperClass.assetNames[assetBalance.Key]} - {assetBalance.Value}");
    }

    Console.WriteLine("\nChanges in values: ");
    foreach (var valueChanged in valueChanges)
    {
        Console.WriteLine($"{valueChanged.Key} - {valueChanged.Value}%");
    }

    if (allWallets[walletAddress].WalletType is not "bitcoin")
    {
        // vrijednost NFA u USD
    }

    ResultOfAction("Success");
    return;
}


static string AddressInput(Dictionary<string, IWallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, string typeOfAddress, string message)
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

        case "asset":
            while (true)
            {
                string? assetAddressString = Console.ReadLine();
                
                Console.WriteLine(HelperClass.assetNames.ContainsKey(Guid.Parse(assetAddressString!)));
                if (HelperClass.assetNames.ContainsKey(Guid.Parse(assetAddressString!)) || HelperClass.NFassetNames.ContainsKey(Guid.Parse(assetAddressString!))) // fungibleAssetList.ContainsKey(assetAddressString!) || nonFungibleAssetList.ContainsKey(assetAddressString!))
                {
                    return assetAddressString!;
                }
            }

        default:
            return "";
    }
}


static void Transfer(Dictionary<string, IWallet> allWallets, string SenderWalletAddress, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    Console.Clear();

    string receiverWalletAddress = AddressInput(allWallets, fungibleAssetList, nonFungibleAssetList, "wallet", "reciever wallet");
    Console.WriteLine("s");
    string assetAddressString = AddressInput(allWallets, fungibleAssetList, nonFungibleAssetList, "asset", "the asset you want to transfer");
    Guid assetAddress = Guid.Parse(assetAddressString);

    Random r = new();
    double percentage = HelperClass.NextDouble(r, -0.5, 0.5);
    bool fungible = HelperClass.assetNames.ContainsKey(assetAddress);

    if (fungible)
    {
        double amount;
        while (true)
        {
            Console.Write("\nEnter the amount you want to transfer: ");

            if (double.TryParse(Console.ReadLine(), out amount))
            {
                break;
            }
        }

        if (allWallets[SenderWalletAddress].CreateNewTransaction(allWallets[receiverWalletAddress], assetAddress, amount))
        {
            fungibleAssetList[HelperClass.assetNames[assetAddress]].Value += fungibleAssetList[HelperClass.assetNames[assetAddress]].Value * percentage;
            ResultOfAction("Success");
            return;
        }
        ResultOfAction("Transaction Failed.");
        return;
    }

    if (allWallets[SenderWalletAddress].WalletType is "ethereum")
    {
        EthereumWallet wallet = (EthereumWallet)allWallets[SenderWalletAddress];
        wallet.CreateNewTransaction(allWallets[receiverWalletAddress], assetAddress, 1);
        ResultOfAction("Success");
        return;
    }
    SolanaWallet wallet1 = (SolanaWallet)allWallets[SenderWalletAddress];
    wallet1.CreateNewTransaction(allWallets[receiverWalletAddress], assetAddress, 1);
    ResultOfAction("Success");
    return;
}

static void TransactionHistory(Dictionary<string, IWallet> allWallets, string walletAddress)
{
    Console.Clear();
    foreach (var transaction in allWallets[walletAddress].TransactionHistory)
    {
        Console.WriteLine($"{transaction}\n");
    }

    Console.WriteLine("1 - Revoke a transaction\n2 - Return to main menu");

    if (UserInput("a number to navigate the menu") is  2)
    {
        return;
    }
    
    string? transactionID;
    while (true)
    {
        Console.Write("\nEnter the ID of the transaction you want to revoke: ");
        transactionID = Console.ReadLine();
        if (!Guid.TryParse(transactionID, out Guid transactionIDGuid))
        {
            continue;
        }
        if (!allWallets[walletAddress].TransactionHistory.ContainsKey(transactionIDGuid))
        {
            continue;
        }

        ITransaction transaction = allWallets[walletAddress].TransactionHistory[transactionIDGuid];
        IWallet sender = allWallets[transaction.SenderAddress.ToString()];
        IWallet receiver = allWallets[transaction.ReceiverAddress.ToString()];

        if (!transaction.RevokeTransaction(sender, receiver))
        {
            ResultOfAction("Revoking failed");
            return;
        }

        ResultOfAction("Success");
        return;

    }
}


static void SeedData(Dictionary<string, IWallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
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

    foreach (var asset in fungibleAssetList)
    {
        HelperClass.assetNames.Add(asset.Value.Address, asset.Key);
    }

    BitcoinWallet bitcoinWallet1 = new(fungibleAssetList);
    BitcoinWallet bitcoinWallet2 = new(fungibleAssetList);
    BitcoinWallet bitcoinWallet3 = new(fungibleAssetList);
    EthereumWallet ethereumWallet1 = new(fungibleAssetList, nonFungibleAssetList);
    EthereumWallet ethereumWallet2 = new(fungibleAssetList, nonFungibleAssetList);
    EthereumWallet ethereumWallet3 = new(fungibleAssetList, nonFungibleAssetList);
    SolanaWallet solanaWallet1 = new(fungibleAssetList, nonFungibleAssetList);
    SolanaWallet solanaWallet2 = new(fungibleAssetList, nonFungibleAssetList);
    SolanaWallet solanaWallet3 = new(fungibleAssetList, nonFungibleAssetList);


    allWallets.Add(bitcoinWallet1.Address.ToString(), bitcoinWallet1);
    allWallets.Add(bitcoinWallet2.Address.ToString(), bitcoinWallet2);
    allWallets.Add(bitcoinWallet3.Address.ToString(), bitcoinWallet3);
    allWallets.Add(ethereumWallet1.Address.ToString(), ethereumWallet2);
    allWallets.Add(ethereumWallet2.Address.ToString(), ethereumWallet2);
    allWallets.Add(ethereumWallet3.Address.ToString(), ethereumWallet3);
    allWallets.Add(solanaWallet1.Address.ToString(), solanaWallet1);
    allWallets.Add(solanaWallet2.Address.ToString(), solanaWallet2);
    allWallets.Add(solanaWallet3.Address.ToString(), solanaWallet3);

    nonFungibleAssetList.Add("Moonbirds #1748", new NonFungibleAsset("Moonbirds #1748", 8.74, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("TerraformsLevel13", new NonFungibleAsset("TerraformsLevel13", 0.48, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Based Ghoul #5229", new NonFungibleAsset("Based Ghoul #5229", 0.12, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Character #2716", new NonFungibleAsset("Character #2716", 7.6, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Character #4241", new NonFungibleAsset("Character #4241", 5.1, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Character #586", new NonFungibleAsset("Character #586", 2.2, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Character #2671", new NonFungibleAsset("Character #2671", 0.55, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Where is Nyan ?", new NonFungibleAsset("Where is Nyan ?", 1.09, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("Celestial Reflections", new NonFungibleAsset("Celestial Reflections", 2.0, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("That Friday Night", new NonFungibleAsset("That Friday Night", 2.7, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #228", new NonFungibleAsset("PokePxls #228", 0.01, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #147", new NonFungibleAsset("PokePxls #147", 0.02, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #135", new NonFungibleAsset("PokePxls #135", 0.03, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #38", new NonFungibleAsset("PokePxls #38", 0.05, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #150", new NonFungibleAsset("PokePxls #150", 0.06, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #104", new NonFungibleAsset("PokePxls #104", 0.03, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("PokePxls #197GOLD", new NonFungibleAsset("PokePxls #197GOLD", 0.5, fungibleAssetList["ethereum"].Address, "ethereum"));
    nonFungibleAssetList.Add("X471", new NonFungibleAsset("X471", 322.19, fungibleAssetList["bitcoin"].Address, "bitcoin"));
    nonFungibleAssetList.Add("Hero Chest", new NonFungibleAsset("Hero Chest", 0.03, fungibleAssetList["bitcoin"].Address, "bitcoin"));
    nonFungibleAssetList.Add("Enjin", new NonFungibleAsset("Enjin", 36.46, fungibleAssetList["solana"].Address, "solana"));

    foreach (var asset in nonFungibleAssetList)
    {
        HelperClass.NFassetNames.Add(asset.Value.Address, asset.Key);
    }
}


Dictionary<string, List<string>> menuOptions = new()
            {
                {"main menu", new List<string>(){ "Create wallet", "Access wallet", "Exit application"} },
                {"create wallet submenu", new List<string>() { "Bitcoin wallet", "Ethereum wallet", "Solana wallet", "Return to main menu"} },
                {"access wallet submenu", new List<string>() { "Portfolio", "Transfer", "Transaction history", "Return to main menu" } }
            };

Dictionary<string, IWallet> allWallets = new(); 

Dictionary<string, FungibleAsset> fungibleAssetList = new();
Dictionary<string, NonFungibleAsset> nonFungibleAssetList = new();

SeedData(allWallets, fungibleAssetList, nonFungibleAssetList);
MainMenu(menuOptions, fungibleAssetList, nonFungibleAssetList, allWallets);