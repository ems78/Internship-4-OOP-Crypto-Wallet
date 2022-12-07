using CryptoWallet.Classes.Wallets;

namespace CryptoWallet.Interfaces
{
    public interface IWallet
    {
        Guid Address { get; }
        string WalletType { get; }
        Dictionary<Guid, double> AssetBalances { get; }
        Dictionary<Guid, double> OwnedNonFungibleAssets { get; }
        List<Guid> AllowedFungibleAssets { get; }
        List<Guid> AllowedNonFungibleAssets { get; }
        Dictionary<Guid, ITransaction> TransactionHistory { get; }
        double TotalValueInUSD();
        bool CreateNewTransaction(Wallet receiverWallet, Guid assetAddress, double amount);
    }
}
