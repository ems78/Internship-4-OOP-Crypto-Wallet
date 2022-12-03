﻿using CryptoWallet.Classes.Transactions;

namespace CryptoWallet.Classes.Wallets
{
    public class EthereumWallet : Wallet
    {
        public new Dictionary<Guid, string> OwnedNonFungibleAssets { get; private set; }

        public EthereumWallet(List<Guid> allowedFungibleAssets) : base(allowedFungibleAssets)
        {
            OwnedNonFungibleAssets = new Dictionary<Guid, string>();
        }

        public EthereumWallet(List<Guid> allowedFungibleAssets, Dictionary<Guid, string> ownedNonFungibleAssets) : base(allowedFungibleAssets)
        {
            OwnedNonFungibleAssets = ownedNonFungibleAssets;
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
