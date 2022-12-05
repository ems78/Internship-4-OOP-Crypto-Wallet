using CryptoWallet.Classes.Assets;

namespace CryptoWallet.Interfaces
{
    public interface IWallet
    {
        Guid Address { get; }
        string WalletType { get; }
        Dictionary<Guid, double> AssetBalances { get; }
        Dictionary<Guid, double> OwnedNonFungibleAssets { get;}   // ovo nebi smilo bit ode? ***
        List<Guid> AllowedFungibleAssets { get; }
        List<Guid> AllowedNonFungibleAssets { get; }
        Dictionary<Guid, ITransaction> TransactionHistory { get; }
        double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList);
        bool CreateNewTransaction(IWallet receiverWallet, Guid assetAddress, double amount);
        bool AddNewTransaction(IWallet senderWallet, Guid assetAddress, ITransaction transaction);
    }
}
