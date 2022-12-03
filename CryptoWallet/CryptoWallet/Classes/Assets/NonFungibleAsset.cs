namespace CryptoWallet.Classes.Assets
{
    public class NonFungibleAsset : Asset
    {
        public double Value { get; private set; }

        public Guid AddressOfValue { get; }  // adresa fa na koju se vrijednost odnosi

        public NonFungibleAsset(string name, double value, Guid addressOfAssetValueRefersTo) : base(name)
        {
            Value = value;
            AddressOfValue = addressOfAssetValueRefersTo;
        }
    }
}
