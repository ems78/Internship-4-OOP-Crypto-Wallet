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
        List<Guid>? OwnedNonFungibleAssets { get;}   // ovo nebi smilo bit ode ***
        List<Guid> AllowedFungibleAssets { get; }
        //ArrayList TransactionHistory { get; }
        Dictionary<Guid, ITransaction> TransactionHistory { get; }
        bool CreateNewFungibleAssetTransactionRecord(IWallet receiverWallet, Guid assetAddress, double amount);
        bool AddTransactionRecord(IWallet senderWallet, Guid assetAddress, FungibleAssetTransaction newTransaction);
        bool AddNonFungibleAssetTransactionRecord(IWallet senderWallet, Guid assetAddress, NonFungibleAssetTransaction newTransaction);
        double TotalValueInUSD(Dictionary<string, FungibleAsset> fungibleAssetList);
    }
}
