namespace CryptoWallet.Classes.Assets
{
    public class FungibleAsset : Asset
    {
        public string Abbreviation { get; }  //  unique

        public double Value { get; set; }  // ?? 

        public FungibleAsset(string name, string abbreviation, double value) : base(name)
        {
            Abbreviation = abbreviation;
            Value = value;
        }

        public double GetValueInUSD()
        {
            return Value;
        }
    }
}
