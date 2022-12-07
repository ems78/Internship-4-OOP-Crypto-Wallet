namespace CryptoWallet.Classes.Assets
{
    public class FungibleAsset : Asset
    {
        public string Abbreviation { get; }

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
            Value *= (1 + percentage / 100);
            ValueChange = percentage;
        }
    }
}
