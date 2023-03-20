using NBitcoin;
using NBitcoin.DataEncoders;
using Nethereum.Signer;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        NBitcoin.Mnemonic mnemonic = new NBitcoin.Mnemonic(Wordlist.English);
        Debug.Log("mnemonic: " + mnemonic);

        byte[] seed = mnemonic.DeriveSeed();
        ExtKey node = ExtKey.CreateFromSeed(seed);

        // m / purpose' / coin_type' / account' / change / address_index
        // m / 44' / 60' / 0' / 0 / 0
        ExtKey accountNode = node.Derive(44, true).Derive(60, true).Derive(0, true);
        ExtKey key = accountNode.Derive(0, false).Derive(0, false);

        string privKeyHex = Encoders.Hex.EncodeData(key.PrivateKey.ToBytes());
        EthECKey ethKey = new EthECKey(privKeyHex);
        string address = ethKey.GetPublicAddress();
        Debug.Log("address: " + address);
    }
}
