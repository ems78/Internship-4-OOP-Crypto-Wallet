using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public class SolanaWallet : NonFungibleAssetSupportedWallet
    {
        public SolanaWallet(Dictionary<string, FungibleAsset> fungibleAssetList) : base(fungibleAssetList) { }
    }
}
