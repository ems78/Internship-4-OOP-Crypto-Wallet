using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public class EthereumWallet : NonFungibleAssetSupportedWallet
    {
        private List<string> _additionalAllowedAssetNames = new()
        {
            "polygon", "bnb", "shibainu"
        };

        public EthereumWallet(Dictionary<string, FungibleAsset> fungibleAssetList) : base(fungibleAssetList) 
            
        {
            foreach (var item in _additionalAllowedAssetNames)
            {
                AssetBalance.Add(fungibleAssetList[item].Address, 5);
            }             
        }
    }
}
