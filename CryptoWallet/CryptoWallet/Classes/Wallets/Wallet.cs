using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Wallets
{
    public abstract class Wallet : IWallet
    {
        public Guid Address { get; }

        public string WalletType { get; protected set; }

        public Dictionary<Guid, double> AssetBalances { get; protected set; }

        public Dictionary<Guid, double> OwnedNonFungibleAssets { get; protected set; }

        public List<Guid> AllowedNonFungibleAssets { get; protected set; }

        public List<Guid> AllowedFungibleAssets { get; protected set; }

        public Dictionary<Guid, ITransaction> TransactionHistory { get; private set; }

        public Wallet(Dictionary<string, FungibleAsset> fungibleAssetList)
        {
            Address = Guid.NewGuid();
            WalletType = "";
            AllowedFungibleAssets = new();
            AllowedNonFungibleAssets = new();
            OwnedNonFungibleAssets = new();
            AssetBalances = new();
            foreach (var item in fungibleAssetList)
            {
                AllowedFungibleAssets.Add(item.Value.Address);
                AssetBalances.Add(item.Value.Address, HelperClass.NextDouble(new Random(), 0, 1.2));
            }
            TransactionHistory = new();
        }

        private void AddToBalance(Guid assetAddress, double amount)
        {
            AssetBalances[assetAddress] += amount;
        }

        private void SubtractFromBalance(Guid assetAddress, double amount)
        {
            AssetBalances[assetAddress] -= amount;
        }

        public virtual bool CreateNewTransaction(Wallet receiverWallet, Guid assetAddress, double amount)
        {
            if (AllowedFungibleAssets.Contains(assetAddress))
            {
                if (NewFungibleAssetTransaction(receiverWallet, assetAddress, amount)) return true;
                return false;
            }
            else if (AllowedNonFungibleAssets.Contains(assetAddress))
            {
                if (NewNonFungibleAssetTransaction(receiverWallet, assetAddress)) return true;
                return false;
            }
            return false;
        }

        protected bool NewFungibleAssetTransaction(Wallet receiverWallet, Guid assetAddress, double amount)
        {
            if (amount > AssetBalances[assetAddress]) return false;
            if (!receiverWallet.AllowedFungibleAssets.Contains(assetAddress)) return false;

            FungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet, amount);
            SubtractFromBalance(assetAddress, amount);
            receiverWallet.AddNewTransaction(assetAddress, newTransaction);
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            return true;
        }

        protected bool NewNonFungibleAssetTransaction(Wallet receiverWallet, Guid assetAddress)
        {
            if (!OwnedNonFungibleAssets!.ContainsKey(assetAddress)) return false;
            if (!receiverWallet.AllowedNonFungibleAssets.Contains(assetAddress)) return false;

            NonFungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet);
            OwnedNonFungibleAssets.Remove(assetAddress);
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            return true;
        }

        protected void AddNewTransaction(Guid assetAddress, ITransaction transaciton)
        {
            AddToBalance(assetAddress, transaciton.Amount);
            TransactionHistory.Add(transaciton.Id, transaciton);
            return;
        }

        protected void AddNewNonFungibleTransaction(Guid assetAddress, ITransaction transaction)
        {
            OwnedNonFungibleAssets.Add(assetAddress, 1);
            TransactionHistory.Add(transaction.Id, transaction);
            return;
        }

        public virtual double TotalValueInUSD()
        {
            double totalAmount = 0;
            foreach (var item in AssetBalances)
            {
                totalAmount += item.Value * HelperClass.fungibleAssets[item.Key].GetValueInUSD();
            }
            return Math.Round(totalAmount, 2);
        }

        public virtual void PrintAssetBalances()
        {
            Console.WriteLine("\nBalances:");
            foreach (var assetBalance in AssetBalances)
            {
                FungibleAsset asset = HelperClass.fungibleAssets[assetBalance.Key];
                if (assetBalance.Value is 0)
                {
                    continue;
                }
                Console.WriteLine($"{asset.Name}       \t {Math.Round(assetBalance.Value, 2)} {asset.Abbreviation}  \t  {asset.ValueChange}%");
            }
        }

        public void PrintTransactionHistory()
        {
            foreach (var transactionRecord in TransactionHistory)
            {
                Console.WriteLine($"{transactionRecord.Value}\n");
            }
        }

        public override string ToString()
        {
            return $"{Address} \t{WalletType}  \t  {TotalValueInUSD()}$";
        }
    }
}
