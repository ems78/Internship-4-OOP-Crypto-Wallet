using CryptoWallet.Classes.Wallets;

namespace CryptoWallet.Interfaces
{
    public interface ITransaction
    {
        Guid Id { get; }
        string TransactionType { get; }
        Guid AssetAddress { get; }
        DateTime DateOfTransaction { get; }
        Guid SenderAddress { get; }
        Guid ReceiverAddress { get; }
        double Amount { get; }
        bool IsRevoked { get; }
        bool RevokeTransaction(Wallet requester, Wallet senderWaller, Wallet receiverWallet);
    }
}
