using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes
{
    public static class HelperClass
    {
        public static Dictionary<Guid, string> assetNames = new() { };

        public static Dictionary<Guid, string> NonFungibleAssetNames = new() { };


        public static double NextDouble(this Random RandGenerator, double MinValue, double MaxValue)
        {
            double result = RandGenerator.NextDouble() * (MaxValue - MinValue) + MinValue;
            return Math.Round(result, 2);
        }

        public static Dictionary<string, double> UpdateCryptocurrencyValues(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
        {
            Dictionary<string, double> valueChangeInPercentage = new();

            foreach (var currency in fungibleAssetList)
            {                   
                double percentage = NextDouble(new Random(), -6.7, 6.7);
                valueChangeInPercentage.Add(currency.Key, percentage);

                double newValue = currency.Value.Value * (1 + percentage / 100);
                currency.Value.SetNewValue(newValue);
            }

            foreach (var nonFungibleAsset in nonFungibleAssetList)
            {
                NonFungibleAsset asset = nonFungibleAsset.Value;

                double percentage = valueChangeInPercentage[asset.NameOfCurrenyValue];

                double newValue = nonFungibleAsset.Value.Value * (1 + percentage / 100);
                asset.SetNewValue(newValue);
            }
            return valueChangeInPercentage;
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

                        if (HelperClass.assetNames.ContainsKey(Guid.Parse(assetAddressString!)) || HelperClass.NonFungibleAssetNames.ContainsKey(Guid.Parse(assetAddressString!)))
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
