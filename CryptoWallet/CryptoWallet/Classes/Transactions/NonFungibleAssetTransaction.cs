using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Transactions
{
    public class NonFungibleAssetTransaction : Transaction
    {
        public NonFungibleAssetTransaction(Guid assetAddress, Wallet senderWallet, Wallet receiverWallet) : base(assetAddress, senderWallet, receiverWallet)
        {
            TransactionType = CryptoWallet.TransactionType.nonFungible.ToString();
            IsRevoked = false;
            TransferAsset(assetAddress, senderWallet, receiverWallet);
            HelperClass.NonFungibleAssets[assetAddress].TriggerValueChange();
        }

        private static void TransferAsset(Guid assetAddress, Wallet senderWallet, Wallet receiverWallet)
        {
            senderWallet.OwnedNonFungibleAssets?.Remove(assetAddress);
            receiverWallet.OwnedNonFungibleAssets?.Add(assetAddress, 1);
        }

        public override bool RevokeTransaction(Wallet requester, Wallet senderWaller, Wallet receiverWallet)
        {
            if (requester.Address != senderWaller.Address) return false;
            if (IsRevoked) return false;
            if ((DateTime.Now - DateOfTransaction).TotalSeconds > 45) return false;

            senderWaller?.OwnedNonFungibleAssets?.Add(AssetAddress, 1);
            receiverWallet?.OwnedNonFungibleAssets?.Remove(ReceiverAddress);
            IsRevoked = true;
            return true;
        }

        public override string ToString()
        {
            return $"Transaction ID:  \t{Id}\nDate and time:   \t{DateOfTransaction}\nSender:         \t{SenderAddress}\nReceiver:       \t{ReceiverAddress}\nAsset name:     \t{HelperClass.NonFungibleAssets[AssetAddress].Name}\nIs revoked:     \t{IsRevoked}";
        }
    }
}
