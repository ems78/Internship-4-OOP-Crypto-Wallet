using CryptoWallet.Classes.Wallets;

namespace CryptoWallet.Classes.Transactions
{
    public class FungibleAssetTransaction : Transaction
    {
        public double StartingSenderBalance { get; private set; }

        public double FinalSenderBalance { get; private set; }

        public double StartingReceiverBalance { get; private set; }

        public double FinalReceiverBalance { get; private set; }

        public FungibleAssetTransaction(Guid assetAddress, Wallet senderWallet, Wallet receiverWallet, double transactionAmount) : base(assetAddress, senderWallet, receiverWallet)
        {
            TransactionType = CryptoWallet.TransactionType.fungible.ToString();
            StartingSenderBalance = senderWallet.AssetBalances[assetAddress];
            StartingReceiverBalance = receiverWallet.AssetBalances[assetAddress];
            FinalSenderBalance = CalculateEndingBalance(true, StartingSenderBalance, transactionAmount);
            FinalReceiverBalance = CalculateEndingBalance(false, FinalSenderBalance, transactionAmount);
            Amount = transactionAmount;
            IsRevoked = false;
            HelperClass.fungibleAssets[assetAddress].TriggerValueChange();

        }

        private static double CalculateEndingBalance(bool isSender, double startingAmount, double transactionAmount)
        {
            if (isSender)
                return startingAmount - transactionAmount;

            return startingAmount + transactionAmount;
        }

        public override bool RevokeTransaction(Wallet requester, Wallet senderWallet, Wallet receiverWallet)
        {
            if (requester.Address != senderWallet.Address) return false;
            if (IsRevoked) return false;
            if ((DateTime.Now - DateOfTransaction).TotalSeconds > 45) return false;

            senderWallet.AssetBalances[AssetAddress] = StartingSenderBalance;
            receiverWallet.AssetBalances[AssetAddress] = StartingReceiverBalance;
            IsRevoked = true;
            return true;
        }

        public override string ToString()
        {
            return $"Transaction ID:  \t{Id}\nDate and time:   \t{DateOfTransaction}\nSender:         \t{SenderAddress}\nReceiver:       \t{ReceiverAddress}\nAmount:         \t{Amount} {HelperClass.fungibleAssets[AssetAddress].Abbreviation}\nIs revoked:     \t{IsRevoked}";
        }
    }
}
