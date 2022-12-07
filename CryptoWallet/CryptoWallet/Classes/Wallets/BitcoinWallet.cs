using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes.Wallets
{
    public class BitcoinWallet : Wallet
    {
        public BitcoinWallet(Dictionary<string, FungibleAsset> fungibleAssetList) : base (fungibleAssetList)
        {
            WalletType = CryptoWallet.WalletType.bitcoin.ToString();
        }
    }
}
