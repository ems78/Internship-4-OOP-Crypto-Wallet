namespace CryptoWallet.Classes.Assets
{
    public class FungibleAsset : Asset
    {
        public string Abbreviation { get; }  //  unique

        public FungibleAsset(string name, string abbreviation, double value) : base(name, value)
        {
            Abbreviation = abbreviation;
        }

        public double GetValueInUSD()
        {
            return Value;
        }
    }
}
