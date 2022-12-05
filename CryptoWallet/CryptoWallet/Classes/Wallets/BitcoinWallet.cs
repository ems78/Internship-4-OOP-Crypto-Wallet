using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes.Wallets
{
    public class BitcoinWallet : Wallet
    {
        public BitcoinWallet(Dictionary<string, FungibleAsset> fungibleAssetList) 
        {
            WalletType = CryptoWallet.WalletType.bitcoin.ToString();

            foreach (var item in fungibleAssetList)
            {
                AllowedFungibleAssets.Add(item.Value.Address);
                AssetBalances.Add(item.Value.Address, 5);
            }
        }
    }
}
