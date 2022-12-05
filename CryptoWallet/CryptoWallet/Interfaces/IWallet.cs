using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Transactions;
using CryptoWallet.Classes.Wallets;
using System.Collections;

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
        bool CreateNewTransaction(IWallet receiverWallet, Guid assetAddress, double amount);
        double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList);
        bool AddNewTransaction(IWallet senderWallet, Guid assetAddress, ITransaction transaction);
    }
}
