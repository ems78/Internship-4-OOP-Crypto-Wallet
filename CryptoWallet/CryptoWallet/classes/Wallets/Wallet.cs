

using System;
using System.Collections.Generic;

namespace CryptoWallet.classes
{
    public class Wallet
    {
        public Guid Address { get; }

        // balansi fa koje posjeduje - adresa i kolicina   --> koju strukturu koristit

        List<Guid> AllowedFungibleAssets { get; }  // adrese podrzanih fa

        // adrese transakcija   --> koju strukturu koristit

    }
}
