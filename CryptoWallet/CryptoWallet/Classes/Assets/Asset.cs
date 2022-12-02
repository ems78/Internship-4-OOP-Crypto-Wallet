namespace CryptoWallet.Classes.Assets
{
    public abstract class Asset
    {
        public Guid Address { get; }

        public string Name { get; }  // unique??


        public Asset(string name)
        {
            Address = new Guid();
            Name = name;
        }
    }
}
