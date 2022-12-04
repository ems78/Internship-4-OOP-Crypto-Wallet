using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes
{
    public static class Class1
    {
        public static Dictionary<Guid, string> assetNames = new()
        {

        };

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

                FungibleAsset cryotocurrency = currency.Value;
                currency.Value.Value += currency.Value.Value * percentage / 100;
            }

            foreach (var nonFungibleAsset in nonFungibleAssetList)
            {
                NonFungibleAsset asset = nonFungibleAsset.Value;

                double percentage = valueChangeInPercentage[asset.NameOfCurrenyValue];
                asset.Value += asset.Value * percentage / 100;
            }

            return valueChangeInPercentage;
        }

    }
}
