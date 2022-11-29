
using System;

namespace CryptoWallet.classes.Assets
{
    public class NonFungibleAsset : Asset
    {
        public double Value { get; private set; }

        public Guid AddressOfValue { get; }  // adresa fa na koju se vrijednost odnosi

        public NonFungibleAsset(string name, double value, Guid address) : base(name)
        {
            Value = value;
            AddressOfValue = address;
        }
    }
}
