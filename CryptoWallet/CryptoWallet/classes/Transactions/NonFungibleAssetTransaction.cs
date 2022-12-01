
using System;

namespace CryptoWallet.classes.Transactions
{
    internal class NonFungibleAssetTransaction : Transaction
    {

        public NonFungibleAssetTransaction(Guid assetAddress, DateTime dateOfTransaction, Guid senderAddress, Guid receiverAddress) : base(assetAddress, dateOfTransaction, senderAddress, receiverAddress)
        {
        }
    }
}
