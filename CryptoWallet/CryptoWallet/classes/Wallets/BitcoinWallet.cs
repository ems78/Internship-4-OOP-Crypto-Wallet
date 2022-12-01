using System;
using System.Collections.Generic;

namespace CryptoWallet.classes.Wallets
{
    public class BitcoinWallet : Wallet
    {

        public BitcoinWallet(Dictionary<Guid, double> fungibleAssetBalance, List<Guid> allowedFungibleAssets) : base(fungibleAssetBalance, allowedFungibleAssets)
        {
        }
    }
}
