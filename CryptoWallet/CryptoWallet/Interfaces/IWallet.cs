using CryptoWallet.Classes.Transactions;
using CryptoWallet.Classes.Wallets;
using System.Collections;

namespace CryptoWallet.Interfaces
{
    public interface IWallet
    {
        Guid Address { get; }
        Dictionary<Guid, double> AssetBalances { get; }
        List<Guid>? OwnedNonFungibleAssets { get;}   // ovo nebi smilo bit ode ***
        //List<string> AllowedFungibleAssetNames { get; }
        List<Guid> AllowedFungibleAssets { get; }
        ArrayList TransactionHistory { get; }
        bool CreateNewFungibleAssetTransactionRecord(Wallet receiverWallet, Guid assetAddress, double amount);
        bool AddTransactionRecord(Wallet senderWallet, Guid assetAddress, FungibleAssetTransaction newTransaction);
    }
}
