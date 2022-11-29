
using System;

namespace CryptoWallet.classes.Transactions
{
    public class FungibleAssetTransaction : Transaction
    {
        // pocetni balans walleta koji salje

        // krajnji balans walleta koji salje

        // pocetni balans walleta koji prima

        // krajnji balans walleta koji prima

        public FungibleAssetTransaction(Guid assetAddress, DateTime dateOfTransaction, Guid senderAddress, Guid receiverAddress) : base(assetAddress, dateOfTransaction, senderAddress, receiverAddress)
        {
            
        }
    }
}
