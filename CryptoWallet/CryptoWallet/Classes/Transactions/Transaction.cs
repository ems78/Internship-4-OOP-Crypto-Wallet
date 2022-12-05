using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Transactions
{
    public abstract class Transaction : ITransaction
    {
        public Guid Id { get; }

        public string TransactionType { get; protected set;  } 

        public Guid AssetAddress { get; }

        public DateTime DateOfTransaction { get; }

        public Guid SenderAddress { get; }

        public Guid ReceiverAddress { get; }

        public double Amount { get; protected set; }

        public bool IsRevoked { get; protected set; }

        public Transaction(Guid assetAddress, IWallet senderWallet, IWallet receiverWallet)
        {
            Id = Guid.NewGuid();
            AssetAddress = assetAddress;
            DateOfTransaction = DateTime.Now;
            SenderAddress = senderWallet.Address;
            ReceiverAddress = receiverWallet.Address;
            TransactionType = "";
            IsRevoked = false;
        }


        public virtual bool RevokeTransaction(IWallet senderWallet, IWallet receiverWallet)
        {
            return false ;  
        }
    }
}
