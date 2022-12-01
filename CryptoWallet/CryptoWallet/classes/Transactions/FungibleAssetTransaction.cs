﻿using System;
using System.Collections;

namespace CryptoWallet.classes.Transactions
{
    public class FungibleAssetTransaction : Transaction
    {
        public double StartingSenderBalance { get; private set; }  

        public double FinalSenderBalance { get; private set; } 

        public double StartingReceiverBalance { get; private set;  } 

        public double FinalReceiverBalance { get; private set;  } 

        public FungibleAssetTransaction(Guid assetAddress, DateTime dateOfTransaction, Wallet senderWallet, Wallet receiverWallet, double transactionAmount) : base(assetAddress, dateOfTransaction, senderWallet, receiverWallet)
        {
            StartingSenderBalance = senderWallet.AssetBalance[assetAddress];
            StartingReceiverBalance = receiverWallet.AssetBalance[assetAddress];
            FinalSenderBalance = CalculateEndingBalance(true, StartingSenderBalance, transactionAmount);
            FinalReceiverBalance = CalculateEndingBalance(false, FinalSenderBalance, transactionAmount);
        }

        private double CalculateEndingBalance(bool isSender, double startingAmount, double transactionAmount)
        {
            if (isSender)
                return startingAmount - transactionAmount;
            
            return startingAmount + transactionAmount;
        }

        public override bool RevokeTransaction(Wallet senderWallet, Wallet receiverWallet)
        {
            if (IsRevoked) return false;
            else if ((DateTime.Now - DateOfTransaction).TotalSeconds > 45)
            {   
                // message 
                return false;
            }

            senderWallet.AssetBalance[AssetAddress] = StartingSenderBalance;
            receiverWallet.AssetBalance[AssetAddress] = StartingReceiverBalance;
            return true;
        }
    }
}
