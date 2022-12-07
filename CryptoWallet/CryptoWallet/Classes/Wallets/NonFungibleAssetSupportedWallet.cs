using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes.Wallets
{
    public class NonFungibleAssetSupportedWallet : Wallet
    {
        public NonFungibleAssetSupportedWallet(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, List<NonFungibleAsset> ownedNonFungibleAssets) : base(fungibleAssetList)
        {
            foreach (var item in nonFungibleAssetList)
            {
                AllowedNonFungibleAssets.Add(item.Value.Address);
            }

            foreach (var item in ownedNonFungibleAssets)
            {
                OwnedNonFungibleAssets.Add(item.Address, 1);
            }
        }

        public override double TotalValueInUSD()
        {
            double total = 0;
            foreach (var item in OwnedNonFungibleAssets)
            {
                NonFungibleAsset asset = HelperClass.NonFungibleAssets[item.Key];
                total += asset.Value * HelperClass.fungibleAssets[asset.AddressOfValue].GetValueInUSD();
            }
            return Math.Round(base.TotalValueInUSD() + total, 2);
        }

        public override void PrintAssetBalances()
        {
            base.PrintAssetBalances();
            Console.WriteLine("\n* Non fungible assets:");
            foreach (var item in OwnedNonFungibleAssets)
            {
                NonFungibleAsset asset = HelperClass.NonFungibleAssets[item.Key];
                FungibleAsset assetOfValue = HelperClass.fungibleAssets[asset.AddressOfValue];
                Console.WriteLine($"{asset.Name}      \t{asset.Value} {assetOfValue.Abbreviation}\t     {Math.Round(asset.Value * assetOfValue.GetValueInUSD())} $");
            }
        }
    }
}
