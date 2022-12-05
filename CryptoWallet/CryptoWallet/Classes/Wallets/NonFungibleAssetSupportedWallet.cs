using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Wallets
{
    public class NonFungibleAssetSupportedWallet : Wallet
    {
        public NonFungibleAssetSupportedWallet(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
        {
            WalletType = CryptoWallet.WalletType.ethereum.ToString();

            foreach (var item in fungibleAssetList)
            {
                AllowedFungibleAssets.Add(item.Value.Address);
                AssetBalances.Add(item.Value.Address, 5);
            }

            foreach (var item in nonFungibleAssetList)
            {
                AllowedNonFungibleAssets.Add(item.Value.Address);
                // owned ??
            }
        }
    }
}
