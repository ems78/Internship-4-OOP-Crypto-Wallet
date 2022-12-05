using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes.Transactions
{
    public class FungibleAssetTransaction : Transaction
    {
        public double StartingSenderBalance { get; private set; }

        public double FinalSenderBalance { get; private set; }

        public double StartingReceiverBalance { get; private set; }

        public double FinalReceiverBalance { get; private set; }


        public FungibleAssetTransaction(Guid assetAddress, IWallet senderWallet, IWallet receiverWallet, double transactionAmount) : base(assetAddress, senderWallet, receiverWallet)
        {
            TransactionType = CryptoWallet.TransactionType.fungible.ToString();
            StartingSenderBalance = senderWallet.AssetBalances[assetAddress];
            StartingReceiverBalance = receiverWallet.AssetBalances[assetAddress];
            FinalSenderBalance = CalculateEndingBalance(true, StartingSenderBalance, transactionAmount);
            FinalReceiverBalance = CalculateEndingBalance(false, FinalSenderBalance, transactionAmount);
            Amount = transactionAmount;
            IsRevoked = false;
        }

        private static double CalculateEndingBalance(bool isSender, double startingAmount, double transactionAmount)
        {
            if (isSender)
                return startingAmount - transactionAmount;

            return startingAmount + transactionAmount;
        }

        public override bool RevokeTransaction(IWallet senderWallet, IWallet receiverWallet)
        {
            if (IsRevoked) return false;
            else if ((DateTime.Now - DateOfTransaction).TotalSeconds > 45)
            {
                return false;
            }

            senderWallet.AssetBalances[AssetAddress] = StartingSenderBalance;
            receiverWallet.AssetBalances[AssetAddress] = StartingReceiverBalance;
            IsRevoked = true;
            return true;
        }
    }
}
