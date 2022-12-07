using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes.Wallets
{
    public class NonFungibleAssetSupportedWallet : Wallet
    {
        public NonFungibleAssetSupportedWallet(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList, List<NonFungibleAsset> ownedNonFungibleAssets)
        {
            foreach (var item in fungibleAssetList)
            {
                AllowedFungibleAssets.Add(item.Value.Address);
                AssetBalances.Add(item.Value.Address, HelperClass.NextDouble(new Random(), 0, 1.2));
            }

            foreach (var item in nonFungibleAssetList)
            {
                AllowedNonFungibleAssets.Add(item.Value.Address);
            }

            foreach (var item in ownedNonFungibleAssets)
            {
                OwnedNonFungibleAssets.Add(item.Address, 1);                    
            }
        }

        public override double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
        {
            double total = 0;
            foreach (var item in OwnedNonFungibleAssets)
            {
                string nfaName = HelperClass.NonFungibleAssetNames[item.Key];
                string faValueName = nonFungibleAssetList[nfaName].NameOfCurrenyValue;
                total += nonFungibleAssetList[nfaName].Value * fungibleAssetList[faValueName].Value;
            }
            return Math.Round(base.TotalValueInUSD(fungibleAssetList, nonFungibleAssetList) + total, 2);
        }
    }
}
