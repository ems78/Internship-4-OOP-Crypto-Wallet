using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes.Wallets
{
    public class BitcoinWallet : Wallet
    {

        private readonly List<string> _allowedAssetNames = new()
        {
            "ethereum", "bitcoin", "solana", "xrp",  "tether", "dogecoin", "polygon", "shibainu", "bnb", "cosmos"
        };

        public BitcoinWallet(Dictionary<string, FungibleAsset> fungibleAssetList) 
        {
            WalletType = CryptoWallet.WalletType.bitcoin.ToString();
            foreach (var item in _allowedAssetNames)
            {
                AllowedFungibleAssets.Add(fungibleAssetList[item].Address);
                AssetBalances.Add(fungibleAssetList[item].Address, 5);
            }
        }
    }
}
