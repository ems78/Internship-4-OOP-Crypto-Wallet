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

        public string WalletType { get; set;  } // set??

        public Dictionary<Guid, double> AssetBalances { get; private set; }

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

        public virtual bool CreateNewTransaction(IWallet receiverWallet, Guid assetAddress, double amount)
        {/*
            if (amount > AssetBalances[assetAddress]) return false;

            if (!receiverWallet.AllowedFungibleAssets.Contains(assetAddress)) return false;
            
            FungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet, amount);
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            if (receiverWallet.AddTransactionRecord(this, assetAddress, newTransaction)) return true;
            return false;
            */

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
            // metoda koja provjerava jeli adresa FA ili NFA. ovisno o tome poziva
        }  // ili metodu za FAT ili NFAT

        protected bool NewFungibleAssetTransaction(IWallet receiverWallet, Guid assetAddress, double amount)
        {
            if (amount > AssetBalances[assetAddress]) return false;
            if (!receiverWallet.AllowedFungibleAssets.Contains(assetAddress)) return false;

            FungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet, amount);

            if (receiverWallet.AddNewTransaction((IWallet)this, assetAddress, newTransaction))
            {
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
            TransactionHistory.Add(transaciton.Id, transaciton);
            return true;
        }


        /*
        public bool AddTransactionRecord(IWallet senderWallet, Guid assetAddress, FungibleAssetTransaction newTransaction)
        {
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            return true;
        }

        public bool AddNonFungibleAssetTransactionRecord(IWallet senderWallet, Guid assetAddress, NonFungibleAssetTransaction newTransaction)
        {
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            return true;
        }*/

        public override string ToString() 
        {
            return $"{WalletType} wallet"; 
        }


        public virtual double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList)
        {
            double totalAmount = 0;
            foreach (var item in AssetBalances)
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
