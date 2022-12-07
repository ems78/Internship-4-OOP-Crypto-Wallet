using CryptoWallet.Classes;
using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;


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


static void CreateWalletSubmenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, Dictionary<string, Wallet> allWallets)
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
                    EthereumWallet newWallet = new(fungibleAssetList, nonFungibleAssetList, new List<NonFungibleAsset>());
                    allWallets.Add(newWallet.Address.ToString(), newWallet);
                }
                return;

            case 3: // create solana wallet
                if (UserConfirmation("create a new solana wallet?"))
                {
                    SolanaWallet newWallet = new(fungibleAssetList, nonFungibleAssetList, new List<NonFungibleAsset>());
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


static void AccessWallet(Dictionary<string, List<string>> menuOptions, Dictionary<string, Wallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("Wallet address\t\t\t\tWallet type\t  Total value");
        HelperClass.PrintLine();
        foreach (var wallet in allWallets)
        {
            Console.WriteLine($"{wallet.Value}  \t  {wallet.Value.TotalValueInUSD(fungibleAssetList, nonFungibleAssetList)} $");
        }
        HelperClass.PrintLine();

        string? walletAddress = HelperClass.AddressInput(allWallets, fungibleAssetList, nonFungibleAssetList, "wallet", "a wallet you want to access");

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


static void Portfolio(Dictionary<string, Wallet> allWallets, string walletAddress, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    Console.Clear();
    allWallets.TryGetValue(walletAddress, out Wallet? wallet);


    //Dictionary<string, double> valueChanges = HelperClass.UpdateCryptocurrencyValues(fungibleAssetList, nonFungibleAssetList);
        
    Console.WriteLine($"Total value in USD: {wallet!.TotalValueInUSD(fungibleAssetList, nonFungibleAssetList)}$ \n\nBalances:");
    HelperClass.PrintLine();
    foreach (var assetBalance in wallet.AssetBalances)
    {
        if (assetBalance.Value is 0)
        {
            continue;
        }
        Console.WriteLine($"{HelperClass.assetNames[assetBalance.Key]}       \t {assetBalance.Value} {fungibleAssetList[HelperClass.assetNames[assetBalance.Key]].Abbreviation}");
    }
    /*
    Console.WriteLine("\nChanges in values: ");
    HelperClass.PrintLine();
    foreach (var valueChanged in valueChanges)
    {
        Console.WriteLine($"{valueChanged.Key} - {valueChanged.Value}%");
    }
    */
    HelperClass.PrintLine();

    if (allWallets[walletAddress].WalletType is not "bitcoin")
    {
        foreach (var item in allWallets[walletAddress].OwnedNonFungibleAssets)
        {
            Console.WriteLine($"{HelperClass.NonFungibleAssetNames[item.Key]}\n");
        }
    }
    ResultOfAction("Success");
    return;
}


static void Transfer(Dictionary<string, Wallet> allWallets, string SenderWalletAddress, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
{
    Console.Clear();

    string receiverWalletAddress = HelperClass.AddressInput(allWallets, fungibleAssetList, nonFungibleAssetList, "wallet", "reciever wallet");
    Console.WriteLine("s");
    string assetAddressString = HelperClass.AddressInput(allWallets, fungibleAssetList, nonFungibleAssetList, "asset", "the asset you want to transfer");
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
            double newValue = fungibleAssetList[HelperClass.assetNames[assetAddress]].Value * (1 + percentage / 100);
            fungibleAssetList[HelperClass.assetNames[assetAddress]].SetNewValue(newValue);
            ResultOfAction("Success");
            return;
        }
        ResultOfAction("Transaction Failed.");
        return;
    }

    NonFungibleAsset asset = nonFungibleAssetList[HelperClass.NonFungibleAssetNames[assetAddress]];
    FungibleAsset assetValueInAsset = fungibleAssetList[HelperClass.assetNames[asset.AddressOfValue]];
    Console.WriteLine($"Old value {assetValueInAsset.Value}"); 
    assetValueInAsset.SetNewValue(assetValueInAsset.Value * (1 + percentage / 100));
    Console.WriteLine($"New value {Math.Round(assetValueInAsset.Value)}");

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

static void TransactionHistory(Dictionary<string, Wallet> allWallets, string walletAddress)
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
        Wallet sender = allWallets[transaction.SenderAddress.ToString()];
        Wallet receiver = allWallets[transaction.ReceiverAddress.ToString()];

        if (!transaction.RevokeTransaction(sender, receiver))
        {
            ResultOfAction("Revoking failed");
            return;
        }

        ResultOfAction("Success");
        return;

    }
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
DataGeneration.GenerateData(allWallets, fungibleAssetList, nonFungibleAssetList);

MainMenu(menuOptions, fungibleAssetList, nonFungibleAssetList, allWallets);