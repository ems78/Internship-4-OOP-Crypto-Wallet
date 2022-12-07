using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;

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
        bool CreateNewTransaction(Wallet receiverWallet, Guid assetAddress, double amount);
        //bool AddNewTransaction(Guid assetAddress, ITransaction transaction);
        //bool AddNewNonFungibleTransaction(Guid assetAddress, ITransaction transaction);
    }
}
