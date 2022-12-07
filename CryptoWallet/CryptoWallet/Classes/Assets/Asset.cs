namespace CryptoWallet.Classes.Assets
{
    public abstract class Asset
    {
        public Guid Address { get; }

        public string Name { get; } 

        public double Value { get; protected set; }

        public double OldValue { get; protected set; }

        public double ValueChange { get; protected set; }

        public Asset(string name, double value)
        {
            Address = Guid.NewGuid();
            Name = name;
            Value = value;
            OldValue = value;
            ValueChange = 0;
        }

        public void SetNewValue(double value)
        {
            Value= value;
        }

        public abstract void TriggerValueChange();
    }
}
