

namespace CryptoWallet.classes.Assets
{
    public class FungibleAsset : Asset
    {
        public string Abbreviation { get; }  //  unique

        public double Value { get; private set; }

        public FungibleAsset(string name, string abbreviation, double value) : base(name)
        {
            Abbreviation = abbreviation;
            Value = value;
        }
    }
}
