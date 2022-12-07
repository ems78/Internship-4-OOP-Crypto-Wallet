namespace CryptoWallet.Classes.Assets
{
    public class NonFungibleAsset : Asset
    {
        public Guid AddressOfValue { get; }

        public string NameOfCurrenyValue { get; private set; }

        FungibleAsset ReferredValueCurrency { get; set; }

        public NonFungibleAsset(string name, double value, FungibleAsset ReferredCurrency, string nameOfCurrenyValue) : base(name, value)
        {
            AddressOfValue = ReferredCurrency.Address;
            NameOfCurrenyValue = nameOfCurrenyValue;
            ReferredValueCurrency = ReferredCurrency;
        }

        public override void TriggerValueChange()
        {
            ReferredValueCurrency.TriggerValueChange();
        }
    }
}
