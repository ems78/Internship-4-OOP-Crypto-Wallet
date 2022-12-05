namespace CryptoWallet.Classes.Assets
{
    public abstract class Asset
    {
        public Guid Address { get; }

        public string Name { get; }  // unique??

        public double Value { get; private set; }

        public Asset(string name, double value)
        {
            Address = Guid.NewGuid();
            Name = name;
            Value = value;
        }

        public void SetNewValue(double value)
        {
            Value = value;
        }
    }
}
