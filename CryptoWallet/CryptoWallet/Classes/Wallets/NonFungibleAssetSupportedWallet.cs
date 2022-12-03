using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public class NonFungibleAssetSupportedWallet : Wallet
    {
        public new Dictionary<Guid, string> OwnedNonFungibleAssets { get; private set; }

        private List<string> _allowedAssetNames = new()
        {
            "ethereum", "bitcoin", "solana", "xrp",  "tether"
        };

        public NonFungibleAssetSupportedWallet(Dictionary<string, FungibleAsset> fungibleAssetList)
        {
            foreach (var item in _allowedAssetNames)
            {
                AssetBalance.Add(fungibleAssetList[item].Address, 5);
            }
            OwnedNonFungibleAssets = new Dictionary<Guid, string>();
        }

        public bool CreateNewNonFungibleTransaction(Wallet receiverWallet, Guid assetAddress)
        {
            if (!OwnedNonFungibleAssets.ContainsKey(assetAddress))
            {
                return false;
            }
            if (!receiverWallet.AllowedAssets.Contains(assetAddress) || receiverWallet.OwnedNonFungibleAssets!.Contains(assetAddress))
            {
                return false;
            }

            NonFungibleAssetTransaction newTransaciton = new(assetAddress, this, receiverWallet);
            TransactionHistory.Add(newTransaciton);
            if (receiverWallet.AddNonFungibleAssetTransactionRecord(this, assetAddress, newTransaciton)) return true;
            return false;
        }
    }
}
