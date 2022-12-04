using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using System.Collections;

namespace CryptoWallet.Classes.Wallets
{
    public abstract class Wallet
    {
        public Guid Address { get; }

        public Dictionary<Guid, double> AssetBalance { get; private set; }

        public List<Guid>? OwnedNonFungibleAssets { get; private set; }

        private List<string> _allowedAssetNames = new();

        public List<Guid> AllowedAssets { get; }

        public ArrayList TransactionHistory { get; private set; }

        public Wallet()
        {
            Address = Guid.NewGuid();
            AllowedAssets = new();
            AssetBalance = new();
            TransactionHistory = new ArrayList();
        }


        public virtual bool CreateNewFungibleAssetTransactionRecord(Wallet receiverWallet, Guid assetAddress, double amount)
        {
            if (amount > AssetBalance[assetAddress])
            {
                return false;
            }
            if (!receiverWallet.AllowedAssets.Contains(assetAddress))
            {
                return false;
            }

            FungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet, amount);
            TransactionHistory.Add(newTransaction);
            if (receiverWallet.AddTransactionRecord(this, assetAddress, newTransaction)) return true;
            return false;
        }


        public bool AddTransactionRecord(Wallet senderWallet, Guid assetAddress, FungibleAssetTransaction newTransaction)
        {
            TransactionHistory.Add(newTransaction);
            return true;
        }

        public bool AddNonFungibleAssetTransactionRecord(Wallet senderWallet, Guid assetAddress, NonFungibleAssetTransaction newTransaction)
        {
            TransactionHistory.Add(newTransaction);
            return true;
        }

        public override string ToString() // enumerable
        {
            return $"--type-- wallet,\t\t--value change---";  // printanje usd vrijednosti
        }



        public virtual double FungibleAssetsTotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList)
        {
            double totalAmount = 0;
            foreach (var item in AssetBalance)
            {
                if (item.Value is 0)
                {
                    continue;
                }
                totalAmount += item.Value * fungibleAssetList[item.Key.ToString()].GetValueInUSD();
            }

            return totalAmount;
        }
    }
}
