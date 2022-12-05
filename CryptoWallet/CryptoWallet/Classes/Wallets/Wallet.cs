using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using CryptoWallet.Interfaces;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;

namespace CryptoWallet.Classes.Wallets
{
    public abstract class Wallet : IWallet
    {
        public Guid Address { get; }

        public string WalletType { get; protected set;  } 

        public Dictionary<Guid, double> AssetBalances { get; protected set; }

        public Dictionary<Guid, double> OwnedNonFungibleAssets { get; protected set; }

        public List<Guid> AllowedNonFungibleAssets { get; protected set; }

        public List<Guid> AllowedFungibleAssets { get; protected set; }

        public Dictionary<Guid, ITransaction> TransactionHistory { get; private set; }

        public Wallet()
        {
            Address = Guid.NewGuid();
            WalletType = "";
            AllowedFungibleAssets = new();
            AllowedNonFungibleAssets = new();
            OwnedNonFungibleAssets = new();
            AssetBalances = new();
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

        public virtual bool CreateNewTransaction(IWallet receiverWallet, Guid assetAddress, double amount)
        {
            if (AllowedFungibleAssets.Contains(assetAddress))
            {
                if (NewFungibleAssetTransaction(receiverWallet, assetAddress, amount)) return true;
                Console.WriteLine("1");
                return false;
            }
            else if (AllowedNonFungibleAssets.Contains(assetAddress))
            {
                if (NewNonFungibleAssetTransaction(receiverWallet, assetAddress)) return true;
                Console.WriteLine("2");
                return false;
            }
            Console.WriteLine("3");
            return false;
        }  


        protected bool NewFungibleAssetTransaction(IWallet receiverWallet, Guid assetAddress, double amount)
        {
            if (amount > AssetBalances[assetAddress]) return false;
            if (!receiverWallet.AllowedFungibleAssets.Contains(assetAddress)) return false;

            FungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet, amount);

            if (receiverWallet.AddNewTransaction((IWallet)this, assetAddress, newTransaction))
            {
                SubtractFromBalance(assetAddress, amount);
                TransactionHistory.Add(newTransaction.Id, newTransaction);
                return true;
            }
            return false;
        }


        protected bool NewNonFungibleAssetTransaction(IWallet receiverWallet, Guid assetAddress)
        {
            if (!OwnedNonFungibleAssets!.ContainsKey(assetAddress)) return false;
            if (!receiverWallet.AllowedNonFungibleAssets.Contains(assetAddress)) return false;

            NonFungibleAssetTransaction newTransaction = new(assetAddress, (IWallet)this, receiverWallet);
            
            if (receiverWallet.AddNewTransaction(this, assetAddress, newTransaction))
            {
                TransactionHistory.Add(newTransaction.Id, newTransaction);
                return true;
            }
            return false;
        }


        public bool AddNewTransaction(IWallet senderWallet, Guid assetAddress, ITransaction transaciton)
        {
            AddToBalance(assetAddress, transaciton.Amount);
            TransactionHistory.Add(transaciton.Id, transaciton);
            return true;
        }


        public override string ToString() 
        {
            return $"{Address} \t{WalletType}"; 
        }


        public virtual double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
        {
            double totalAmount = 0;
            foreach (var currencyKeyValuePair in AssetBalances)
            {
                string assetName = HelperClass.assetNames[currencyKeyValuePair.Key];
                totalAmount += currencyKeyValuePair.Value * fungibleAssetList[assetName].GetValueInUSD();
            }
            return Math.Round(totalAmount, 2);
        }
    }
}
