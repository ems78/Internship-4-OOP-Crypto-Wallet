namespace CryptoWallet
{
    public enum WalletType
    {
        bitcoin,
        ethereum,
        solana
    }

    public enum AllowedFungibleAssetNames
    {
        bitcoin,
        ethereum,
        solana,
        xrp,
        tether,
        // dodat ostale
    }

    public enum TransactionType
    {
        fungible,
        nonFungible
    }
}
