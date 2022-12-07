namespace CryptoWallet.Classes.Assets
{
    public abstract class Asset
    {
        public Guid Address { get; }

        public string Name { get; }

        public double Value { get; protected set; }

        public double ValueChange { get; protected set; }

        public Asset(string name, double value)
        {
            Address = Guid.NewGuid();
            Name = name;
            Value = value;
            ValueChange = 0;
        }

        public abstract void TriggerValueChange();
    }
}
