using CryptoWallet.Classes.Assets;
using CryptoWallet.Classes.Wallets;
using CryptoWallet.Interfaces;

namespace CryptoWallet.Classes
{
    public static class DataGeneration
    {
        public static void GenerateData(Dictionary<string, Wallet> allWallets, Dictionary<string, FungibleAsset> fungibleAssetList, Dictionary<string, NonFungibleAsset> nonFungibleAssetList)
        {
            fungibleAssetList.Add("bitcoin", new FungibleAsset("bitcoin", "BTC", 16989.53));
            fungibleAssetList.Add("xrp", new FungibleAsset("xrp", "XRP", 0.39));
            fungibleAssetList.Add("dogecoin", new FungibleAsset("dogecoin", "DOGE", 0.1));
            fungibleAssetList.Add("ethereum", new FungibleAsset("ethereum", "ETH", 1273.8));
            fungibleAssetList.Add("polygon", new FungibleAsset("polygon", "MATIC", 0.92));
            fungibleAssetList.Add("tether", new FungibleAsset("tether", "USDT", 1));
            fungibleAssetList.Add("solana", new FungibleAsset("solana", "SOL", 13.57));
            fungibleAssetList.Add("shibainu", new FungibleAsset("shiba inu", "SHIB", 0.00000929));
            fungibleAssetList.Add("bnb", new FungibleAsset("bnb", "BNB", 290.68));
            fungibleAssetList.Add("cosmos", new FungibleAsset("comsos", "ATOM", 10.22));

            foreach (var asset in fungibleAssetList)
            {
                HelperClass.assetNames.Add(asset.Value.Address, asset.Key);
            }

            nonFungibleAssetList.Add("Moonbirds #1748", new NonFungibleAsset("Moonbirds #1748", 8.74, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("TerraformsLevel13", new NonFungibleAsset("TerraformsLevel13", 0.48, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Based Ghoul #5229", new NonFungibleAsset("Based Ghoul #5229", 0.12, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Character #2716", new NonFungibleAsset("Character #2716", 7.6, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Character #4241", new NonFungibleAsset("Character #4241", 5.1, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Character #586", new NonFungibleAsset("Character #586", 2.2, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Character #2671", new NonFungibleAsset("Character #2671", 0.55, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Where is Nyan ?", new NonFungibleAsset("Where is Nyan ?", 1.09, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("Celestial Reflections", new NonFungibleAsset("Celestial Reflections", 2.0, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("That Friday Night", new NonFungibleAsset("That Friday Night", 2.7, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #228", new NonFungibleAsset("PokePxls #228", 0.01, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #147", new NonFungibleAsset("PokePxls #147", 0.02, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #135", new NonFungibleAsset("PokePxls #135", 0.03, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #38", new NonFungibleAsset("PokePxls #38", 0.05, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #150", new NonFungibleAsset("PokePxls #150", 0.06, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #104", new NonFungibleAsset("PokePxls #104", 0.03, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("PokePxls #197GOLD", new NonFungibleAsset("PokePxls #197GOLD", 0.5, fungibleAssetList["ethereum"].Address, "ethereum"));
            nonFungibleAssetList.Add("X471", new NonFungibleAsset("X471", 322.19, fungibleAssetList["bitcoin"].Address, "bitcoin"));
            nonFungibleAssetList.Add("Hero Chest", new NonFungibleAsset("Hero Chest", 0.03, fungibleAssetList["bitcoin"].Address, "bitcoin"));
            nonFungibleAssetList.Add("Enjin", new NonFungibleAsset("Enjin", 36.46, fungibleAssetList["solana"].Address, "solana"));

            BitcoinWallet bitcoinWallet1 = new(fungibleAssetList);
            BitcoinWallet bitcoinWallet2 = new(fungibleAssetList);
            BitcoinWallet bitcoinWallet3 = new(fungibleAssetList);

            List<NonFungibleAsset> ownedAssets1 = new()
            {
                nonFungibleAssetList["Moonbirds #1748"],
                nonFungibleAssetList["That Friday Night"],
                nonFungibleAssetList["PokePxls #38"],
                nonFungibleAssetList["PokePxls #197GOLD"]
            };
            List<NonFungibleAsset> ownedAssets2 = new()
            {
                nonFungibleAssetList["Character #2716"],
                nonFungibleAssetList["Enjin"],
                nonFungibleAssetList["PokePxls #150"],
                nonFungibleAssetList["PokePxls #104"]
            };
            List<NonFungibleAsset> ownedAssets3 = new()
            {
                nonFungibleAssetList["Celestial Reflections"],
                nonFungibleAssetList["Based Ghoul #5229"],
                nonFungibleAssetList["Hero Chest"]
            };

            EthereumWallet ethereumWallet1 = new(fungibleAssetList, nonFungibleAssetList, ownedAssets1);
            EthereumWallet ethereumWallet2 = new(fungibleAssetList, nonFungibleAssetList, ownedAssets2);
            EthereumWallet ethereumWallet3 = new(fungibleAssetList, nonFungibleAssetList, ownedAssets3);

            List<NonFungibleAsset> ownedAssets4 = new()
            {
                nonFungibleAssetList["PokePxls #228"],
                nonFungibleAssetList["Character #4241"],
            };
            List<NonFungibleAsset> ownedAssets5 = new()
            {
                nonFungibleAssetList["Character #2671"],
                nonFungibleAssetList["TerraformsLevel13"],
                nonFungibleAssetList["PokePxls #135"]
            };
            List<NonFungibleAsset> ownedAssets6 = new()
            {
                nonFungibleAssetList["Where is Nyan ?"],
                nonFungibleAssetList["X471"],
                nonFungibleAssetList["PokePxls #147"],
                nonFungibleAssetList["Character #586"]
            };

            SolanaWallet solanaWallet1 = new(fungibleAssetList, nonFungibleAssetList, ownedAssets4);
            SolanaWallet solanaWallet2 = new(fungibleAssetList, nonFungibleAssetList, ownedAssets5);
            SolanaWallet solanaWallet3 = new(fungibleAssetList, nonFungibleAssetList, ownedAssets6);

            allWallets.Add(bitcoinWallet1.Address.ToString(), bitcoinWallet1);
            allWallets.Add(bitcoinWallet2.Address.ToString(), bitcoinWallet2);
            allWallets.Add(bitcoinWallet3.Address.ToString(), bitcoinWallet3);
            allWallets.Add(ethereumWallet1.Address.ToString(), ethereumWallet1);
            allWallets.Add(ethereumWallet2.Address.ToString(), ethereumWallet2);
            allWallets.Add(ethereumWallet3.Address.ToString(), ethereumWallet3);
            allWallets.Add(solanaWallet1.Address.ToString(), solanaWallet1);
            allWallets.Add(solanaWallet2.Address.ToString(), solanaWallet2);
            allWallets.Add(solanaWallet3.Address.ToString(), solanaWallet3);

            foreach (var asset in nonFungibleAssetList)
            {
                HelperClass.NonFungibleAssetNames.Add(asset.Value.Address, asset.Key);
            }
        }
    }
}
