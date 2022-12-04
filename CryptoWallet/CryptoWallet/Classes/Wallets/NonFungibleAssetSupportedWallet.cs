using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Wallets
{
    public class NonFungibleAssetSupportedWallet : Wallet, ISupportsNonFungibleAssets
    {
        public new Dictionary<Guid, string> OwnedNonFungibleAssets { get; private set; }
        public Dictionary<Guid, string> AllowedNonFungibleAssets { get; private set; }

        public List<string> AllowedAssetNames = new()
        {
            "ethereum", "bitcoin", "solana", "xrp",  "tether", "dogecoin", "polygon", "shibainu", "bnb", "cosmos"  // enum?
        };

        public NonFungibleAssetSupportedWallet(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
        {
            foreach (var item in AllowedAssetNames)
            {
                AssetBalances.Add(fungibleAssetList[item].Address, 5);
            }

            AllowedNonFungibleAssets = new();
            foreach (var item in nonFungibleAssetList)
            {
                AllowedNonFungibleAssets.Add(item.Value.Address, item.Value.Name);
            }
            OwnedNonFungibleAssets = new Dictionary<Guid, string>();
        }

        public bool CreateNewNonFungibleTransaction(IWallet receiverWallet, Guid assetAddress)
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
            TransactionHistory.Add(newTransaciton.Id, newTransaciton);
            if (receiverWallet.AddNonFungibleAssetTransactionRecord(this, assetAddress, newTransaciton)) return true;
            return false;
        }

        public override double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList)
        {
            foreach (var item in OwnedNonFungibleAssets)
            {   
                // zbrojit vrijednosti svakog NFA 
            }
            // nadodat na vrijednosti FA
            return base.TotalValueInUSD(fungibleAssetList);
        }
    }
}
