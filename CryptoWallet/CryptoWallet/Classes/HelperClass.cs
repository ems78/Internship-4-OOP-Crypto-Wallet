using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes
{
    public static class HelperClass
    {
        public static Dictionary<Guid, FungibleAsset> fungibleAssets = new() { };

        public static Dictionary<Guid, NonFungibleAsset> NonFungibleAssets = new() { };

        public static double NextDouble(this Random RandGenerator, double MinValue, double MaxValue)
        {
            double result = RandGenerator.NextDouble() * (MaxValue - MinValue) + MinValue;
            return Math.Round(result, 2);
        }

        public static void PrintLine()
        {
            Console.WriteLine("--------------------------------------------------------------------------");
        }

        public static string AddressInput(Dictionary<string, Wallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, string typeOfAddress, string message)
        {
            switch (typeOfAddress)
            {
                case "wallet":
                    while (true)
                    {
                        Console.Write($"\nEnter the address of {message}: ");
                        string? walletAddress = Console.ReadLine();

                        if (allWallets.ContainsKey(walletAddress!))
                        {
                            return walletAddress!;
                        }
                    }

                case "asset":
                    while (true)
                    {
                        Console.Write($"\nEnter the address of {message}: ");
                        string? assetAddressString = Console.ReadLine();

                        if (HelperClass.fungibleAssets.ContainsKey(Guid.Parse(assetAddressString!)) || HelperClass.NonFungibleAssets.ContainsKey(Guid.Parse(assetAddressString!)))
                        {
                            return assetAddressString!;
                        }
                    }

                default:
                    return "";
            }
        }
    }
}
