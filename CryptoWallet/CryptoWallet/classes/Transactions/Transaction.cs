
using System;

namespace CryptoWallet.classes.Transactions
{
    public class Transaction
    {
        public Guid Id { get; }

        public Guid AssetAddress { get; }

        public DateTime DateOfTransaction { get; }

        public Guid SenderAddress { get;  }  // adresa walleta koji salje fungible asset

        public Guid ReceiverAddress { get; }  // adresa walleta koji prima fa
        
        public bool IsRevoked { get; private set; }

        // metoda za opozivanje

        public Transaction(Guid assetAddress, DateTime dateOfTransaction, Guid senderAddress, Guid receiverAddress)
        {
            Id = new Guid();
            AssetAddress = assetAddress;
            DateOfTransaction = dateOfTransaction;
            SenderAddress = senderAddress;
            ReceiverAddress = receiverAddress;
        }
    }
}
