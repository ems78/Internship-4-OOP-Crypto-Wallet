using CryptoWallet.Classes.Assets;

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
            Random randomDouble = new();

            foreach (var currency in fungibleAssetList)
            {                   
                double percentage = NextDouble(randomDouble, -6.7, 6.7);
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
    }
}
