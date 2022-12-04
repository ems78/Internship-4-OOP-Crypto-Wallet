using CryptoWallet.Classes.Wallets;

namespace CryptoWallet.Interfaces
{
    public interface ISupportsNonFungibleAssets : IWallet
    {
        new Dictionary<Guid, string> OwnedNonFungibleAssets { get; }
        Dictionary<Guid, string> AllowedNonFungibleAssets { get; }
        bool CreateNewNonFungibleTransaction(Wallet receiverWallet, Guid assetAddress);
    }
}
