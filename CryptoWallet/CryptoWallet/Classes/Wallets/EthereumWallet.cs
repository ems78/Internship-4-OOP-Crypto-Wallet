using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public class EthereumWallet : NonFungibleAssetSupportedWallet
    {
        public EthereumWallet(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList) : base (fungibleAssetList, nonFungibleAssetList)
        {

        }
    }
}
