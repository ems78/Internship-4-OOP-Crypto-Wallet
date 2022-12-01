using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoWallet.classes
{
    public abstract class Wallet
    {
        public Guid Address { get; }

        public Dictionary<Guid, double> AssetBalance { get; private set; }

        public List<Guid> OwnedNonFungibleAssets { get; private set; } 

        public List<Guid> AllowedAssets { get; }

        public ArrayList TransactionHistory { get; private set; }

        public Wallet(Dictionary<Guid, double> assetBalance, List<Guid> allowedAssets)
        {
            Address = new Guid();
            AssetBalance = assetBalance;
            AllowedAssets = allowedAssets;
            TransactionHistory = new ArrayList();
        }

        public void AddNewTransactionRecord() 
        {
            // --
        }
    }
}
