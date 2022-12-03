using CryptoWallet.Classes.Wallets;

namespace CryptoWallet.Classes.Transactions
{
    public class NonFungibleAssetTransaction : Transaction
    {
        public NonFungibleAssetTransaction(Guid assetAddress, Wallet senderWallet, Wallet receiverWallet) : base(assetAddress, senderWallet, receiverWallet)
        {
            TransferAsset(assetAddress, senderWallet, receiverWallet);
        }

        private void TransferAsset(Guid assetAddress, Wallet senderWallet, Wallet receiverWallet)
        {
            senderWallet.OwnedNonFungibleAssets?.Remove(assetAddress);
            receiverWallet.OwnedNonFungibleAssets?.Add(assetAddress);
        }
    }
}
