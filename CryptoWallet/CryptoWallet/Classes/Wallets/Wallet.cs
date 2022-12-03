using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using System.Collections;
using System.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public abstract class Wallet
    {
        public Guid Address { get; }

        public Dictionary<Guid, double> AssetBalance { get; private set; }

        public List<Guid>? OwnedNonFungibleAssets { get; private set; }

        private List<string> _allowedAssetNames = new List<string>();

        public List<Guid> AllowedAssets { get; }

        public ArrayList TransactionHistory { get; private set; }

        public Wallet()
        {
            Address = Guid.NewGuid();
            AllowedAssets = new();
            AssetBalance = new();/*
            foreach (var item in _allowedAssetNames!)
            {
                AllowedAssets!.Add(fungibleAssetList[item].Address);
                AssetBalance.Add(fungibleAssetList[item].Address, 5);
            }*/
            //InitialiseAssetsBalance();
            TransactionHistory = new ArrayList();
        }

        /*
        private protected void InitialiseAssetsBalance()
        {
            foreach (var allowedAssetAddress in AllowedAssets)
            {
                AssetBalance.Add(allowedAssetAddress, 5);
            }
        }*/


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
    }
}
