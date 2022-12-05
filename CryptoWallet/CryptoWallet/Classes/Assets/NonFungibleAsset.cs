namespace CryptoWallet.Classes.Assets
{
    public class NonFungibleAsset : Asset
    {
        public Guid AddressOfValue { get; }

        public string NameOfCurrenyValue { get; private set; }

        public NonFungibleAsset(string name, double value, Guid addressOfAssetValueRefersTo, string nameOfCurrenyValue) : base(name, value)
        {
            AddressOfValue = addressOfAssetValueRefersTo;
            NameOfCurrenyValue = nameOfCurrenyValue;
        }
    }
}
