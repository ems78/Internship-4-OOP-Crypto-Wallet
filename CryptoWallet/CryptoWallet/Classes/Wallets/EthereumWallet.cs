namespace CryptoWallet.Classes.Wallets
{
    public class EthereumWallet : Wallet
    {
        public new Dictionary<Guid, string> OwnedNonFungibleAssets { get; private set; }

        public EthereumWallet(Dictionary<Guid, double> fungibleAssetBalance, List<Guid> allowedFungibleAssets) : base(fungibleAssetBalance, allowedFungibleAssets)
        {
            OwnedNonFungibleAssets = new Dictionary<Guid, string>();
        }

        public EthereumWallet(Dictionary<Guid, double> fungibleAssetBalance, List<Guid> allowedFungibleAssets, Dictionary<Guid, string> ownedNonFungibleAssets) : base(fungibleAssetBalance, allowedFungibleAssets)
        {
            OwnedNonFungibleAssets = ownedNonFungibleAssets;
        }
    }
}
