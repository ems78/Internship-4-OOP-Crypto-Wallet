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

        public override void TriggerValueChange()
        {
            double percentage = HelperClass.NextDouble(new Random(), -5, 5);
            OldValue = Value;
            Value = OldValue * (1 + percentage);
            ValueChange = percentage;
        }
    }
}
