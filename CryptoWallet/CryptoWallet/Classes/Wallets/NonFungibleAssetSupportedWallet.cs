using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public class NonFungibleAssetSupportedWallet : Wallet
    {
        public new Dictionary<Guid, string> OwnedNonFungibleAssets { get; private set; }
        public Dictionary<Guid, string> AllowedNonFungibleAssets { get; private set; }

        public List<string> AllowedAssetNames = new()
        {
            "ethereum", "bitcoin", "solana", "xrp",  "tether"
        };

        public NonFungibleAssetSupportedWallet(Dictionary<string, FungibleAsset> fungibleAssetList)
        {
            foreach (var item in AllowedAssetNames)
            {
                AssetBalances.Add(fungibleAssetList[item].Address, 5);
            }
            // allowed nfa addresses
            OwnedNonFungibleAssets = new Dictionary<Guid, string>();
        }

        public bool CreateNewNonFungibleTransaction(Wallet receiverWallet, Guid assetAddress)
        {
            if (!OwnedNonFungibleAssets.ContainsKey(assetAddress))
            {
                return false;
            }
            if (!receiverWallet.AllowedFungibleAssets.Contains(assetAddress) || receiverWallet.OwnedNonFungibleAssets!.Contains(assetAddress))
            {
                return false;
            }

            NonFungibleAssetTransaction newTransaciton = new(assetAddress, this, receiverWallet);
            TransactionHistory.Add(newTransaciton);
            if (receiverWallet.AddNonFungibleAssetTransactionRecord(this, assetAddress, newTransaciton)) return true;
            return false;
        }

        /*
        public override double TotalValueInUSD()
        {
            // + non fungible asset value
            return base.TotalValueInUSD();
        }*/
    }
}
