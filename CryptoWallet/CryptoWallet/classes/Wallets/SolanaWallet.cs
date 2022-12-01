using System;
using System.Collections.Generic;

namespace CryptoWallet.classes.Wallets
{
    public class SolanaWallet : Wallet
    {
        public new List<Guid> OwnedNonFungibleAssets { get; private set; } 

        public SolanaWallet(Dictionary<Guid, double> fungibleAssetBalance, List<Guid> allowedFungibleAssets) : base(fungibleAssetBalance, allowedFungibleAssets)
        {
            OwnedNonFungibleAssets = new List<Guid>();
        }

        public SolanaWallet(Dictionary<Guid, double> fungibleAssetBalance, List<Guid> allowedFungibleAssets, List<Guid> ownedNonFungibleAssets) : base(fungibleAssetBalance, allowedFungibleAssets)
        {
            OwnedNonFungibleAssets = ownedNonFungibleAssets;
        }
    }
}
