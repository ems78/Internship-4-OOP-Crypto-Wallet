using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Transactions
{
    public class NonFungibleAssetTransaction : Transaction
    {
        public bool IsRevoked { get; private set; }


        public NonFungibleAssetTransaction(Guid assetAddress, IWallet senderWallet, IWallet receiverWallet) : base(assetAddress, senderWallet, receiverWallet)
        {
            IsRevoked= false;
            TransferAsset(assetAddress, senderWallet, receiverWallet);
        }

        private void TransferAsset(Guid assetAddress, IWallet senderWallet, IWallet receiverWallet)
        {
            senderWallet.OwnedNonFungibleAssets?.Remove(assetAddress);
            receiverWallet.OwnedNonFungibleAssets?.Add(assetAddress);
        }

        public bool RevokeTransaction(IWallet senderWaller, IWallet receiverWallet)
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
        }
    }
}
