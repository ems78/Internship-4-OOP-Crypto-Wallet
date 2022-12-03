using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Classes.Wallets
{
    public class BitcoinWallet : Wallet
    {
        private readonly List<string> _allowedAssetNames = new()
        {
            "bitcoin", "ethereum", "solana", "xrp", "dogecoin", "polygon", "tether", "shibainu", "cosmos"
        };

        public BitcoinWallet(Dictionary<string, FungibleAsset> fungibleAssetList) 
        {
            foreach (var item in _allowedAssetNames)
            {
                AssetBalance.Add(fungibleAssetList[item].Address, 5);
            }
        }
    }
}
