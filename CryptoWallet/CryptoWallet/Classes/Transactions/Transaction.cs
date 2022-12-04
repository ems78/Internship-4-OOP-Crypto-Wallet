using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Transactions
{
    public abstract class Transaction : ITransaction
    {
        public Guid Id { get; }

        public Guid AssetAddress { get; }

        public DateTime DateOfTransaction { get; }

        public Guid SenderAddress { get; }

        public Guid ReceiverAddress { get; }

        //protected bool IsRevoked { get; private set; }

        public Transaction(Guid assetAddress, IWallet senderWallet, IWallet receiverWallet)
        {
            Id = Guid.NewGuid();
            AssetAddress = assetAddress;
            DateOfTransaction = DateTime.Now;
            SenderAddress = senderWallet.Address;
            ReceiverAddress = receiverWallet.Address;
            //IsRevoked= false;
        }

        public override string ToString()
        {
            return $"{Id}\t{DateOfTransaction}\t{SenderAddress}\t{ReceiverAddress}--kolicina--\t--ime asseta--\t--IsRevoked";
        }
        /*
        public virtual bool RevokeTransaction(Wallet senderWaller, Wallet receiverWallet)
        {
            if (IsRevoked)
            {
                return false;
            }
            else if ((DateTime.Now - DateOfTransaction).TotalSeconds > 45)
            {
                return false;
            }

            senderWaller?.OwnedNonFungibleAssets?.Add(AssetAddress);
            receiverWallet?.OwnedNonFungibleAssets?.Remove(ReceiverAddress);
            IsRevoked = true;
            return true;
        }*/
    }
}
