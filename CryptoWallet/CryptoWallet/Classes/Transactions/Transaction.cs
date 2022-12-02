using CryptoWallet.Classes.Wallets;

namespace CryptoWallet.Classes.Transactions
{
    public abstract class Transaction
    {
        public Guid Id { get; }

        public Guid AssetAddress { get; }

        public DateTime DateOfTransaction { get; }

        public Guid SenderAddress { get; }

        public Guid ReceiverAddress { get; }

        public bool IsRevoked { get; private set; }

        public Transaction(Guid assetAddress, DateTime dateOfTransaction, Wallet senderWallet, Wallet receiverWallet)
        {
            Id = new Guid();
            AssetAddress = assetAddress;
            DateOfTransaction = dateOfTransaction;
            SenderAddress = senderWallet.Address;
            ReceiverAddress = receiverWallet.Address;
        }

        public virtual bool RevokeTransaction(Wallet senderWaller, Wallet receiverWallet)
        {
            if (IsRevoked)
            {
                // message?
                return false;
            }
            else if ((DateTime.Now - DateOfTransaction).TotalSeconds > 45)
            {
                // message
                return false;
            }

            senderWaller.OwnedNonFungibleAssets.Add(AssetAddress);
            receiverWallet.OwnedNonFungibleAssets.Remove(ReceiverAddress);
            IsRevoked = true;
            return true;
        }
    }
}
