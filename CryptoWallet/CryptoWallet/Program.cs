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
    Console.Clear();
    int i = 0;
    foreach (var item in menuOptions[menuToPrint])
    {
        Console.WriteLine($"{++i} - {item}");
    }
    // linija
}


static void MainMenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Hashtable allWallets)
{
    while (true)
    {
        PrintMenuOptions(menuOptions, "main menu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1: // create wallet
                CreateWalletSubmenu(menuOptions, fungibleAssetList, allWallets);
                break;

            case 2: // access wallet
                AccessWallet(menuOptions, allWallets);
                break;

            case 3:
                Environment.Exit(0);
                break;

            default:
                break;
        }
    }
}


static void CreateWalletSubmenu(Dictionary<string, List<string>> menuOptions, Dictionary<string, FungibleAsset> fungibleAssetList, Hashtable allWallets)
{
    while (true)
    {
        PrintMenuOptions(menuOptions, "create wallet submenu");

        switch (UserInput("a number to navigate the menu"))
        {
            case 1: // create bitcoin wallet
                if (UserConfirmation("create a new bitcoin wallet?"))
                {
                    BitcoinWallet newWallet = new(fungibleAssetList);
                    allWallets.Add(newWallet.Address, newWallet);
                }
                // press any key...
                return;

            case 2: // create ethereum wallet
                if (UserConfirmation("create a new ethereum wallet?"))
                {
                    EthereumWallet newWallet = new(fungibleAssetList);
                    allWallets.Add(newWallet.Address, newWallet);
                }
                // press any key...
                return;

            case 3: // create solana wallet
                if (UserConfirmation("create a new solana wallet?"))
                {
                    SolanaWallet newWallet = new(fungibleAssetList);
                    allWallets.Add(newWallet.Address, newWallet);
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


static void AccessWallet(Dictionary<string, List<string>> menuOptions, Hashtable allWallets)
{
    // --
}


static void SeedData(Hashtable allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
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

    allWallets.Add(bitcoinWallet1.Address, bitcoinWallet1);
    allWallets.Add(bitcoinWallet2.Address, bitcoinWallet2);
    allWallets.Add(bitcoinWallet3.Address, bitcoinWallet3);

    nonFungibleAssetList.Add("Moonbirds#1748", new NonFungibleAsset("Moonbirds#1748", 8.74, fungibleAssetList["ethereum"].Address));
    nonFungibleAssetList.Add("TerraformsLevel13", new NonFungibleAsset("TerraformsLevel13", 0.48, fungibleAssetList["ethereum"].Address));
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


Dictionary<string, List<string>> menuOptions = new Dictionary<string, List<string>>()
            {
                {"main menu", new List<string>(){ "Create wallet", "Access wallet", "Exit application"} },
                {"create wallet submenu", new List<string>() { "Bitcoin wallet", "Ethereum wallet", "Solana wallet", "Return to main menu"} },
                {"access wallet submenu", new List<string>() { "Portfolio", "Transfer", "Transaction history", "Return to main menu" } }
            };

Hashtable allWallets = new Hashtable();
Dictionary<string, FungibleAsset> fungibleAssetList = new Dictionary<string, FungibleAsset>();
Dictionary<string, NonFungibleAsset> nonFungibleAssetList = new Dictionary<string, NonFungibleAsset>();

SeedData(allWallets, fungibleAssetList, nonFungibleAssetList);
MainMenu(menuOptions, fungibleAssetList, allWallets);