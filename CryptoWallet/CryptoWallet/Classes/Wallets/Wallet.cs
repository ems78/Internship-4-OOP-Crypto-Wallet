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

        public List<Guid>? OwnedNonFungibleAssets { get; private set; }

        //public List<string> AllowedFungibleAssetNames { get; private set; }

        public List<Guid> AllowedFungibleAssets { get; }

        public Dictionary<Guid, ITransaction> TransactionHistory { get; private set; }

        public Wallet()
        {
            Address = Guid.NewGuid();
            WalletType = "";
            AllowedFungibleAssets = new();
            AssetBalances = new();
            TransactionHistory = new();
        }

        public virtual bool CreateNewFungibleAssetTransactionRecord(IWallet receiverWallet, Guid assetAddress, double amount)
        {
            if (amount > AssetBalances[assetAddress]) return false;

            if (!receiverWallet.AllowedFungibleAssets.Contains(assetAddress)) return false;
            
            FungibleAssetTransaction newTransaction = new(assetAddress, this, receiverWallet, amount);
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            if (receiverWallet.AddTransactionRecord(this, assetAddress, newTransaction)) return true;
            return false;
        }

        // metoda koja provjerava jeli adresa FA ili NFA. ovisno o tome poziva
        // ili metodu za FAT ili NFAT


        public bool AddTransactionRecord(IWallet senderWallet, Guid assetAddress, FungibleAssetTransaction newTransaction)
        {
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            return true;
        }

        public bool AddNonFungibleAssetTransactionRecord(IWallet senderWallet, Guid assetAddress, NonFungibleAssetTransaction newTransaction)
        {
            TransactionHistory.Add(newTransaction.Id, newTransaction);
            return true;
        }

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
