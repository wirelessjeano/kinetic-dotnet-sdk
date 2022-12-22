using Kinetic.Example.Models;
using Kinetic.Sdk;

namespace Kinetic.Example;

public static class Utility
{
    public static Keypair GetKeyPair(KeyOrMnemonics keyOrMnemonics)
    {
        keyOrMnemonics.KeyOrMnemonicsValue = keyOrMnemonics.KeyOrMnemonicsValue.Trim().Replace("\u200B", "");
        
        try
        {
            return Keypair.FromMnemonic(keyOrMnemonics.KeyOrMnemonicsValue);
        }
        catch (NotSupportedException)
        {
            return Keypair.FromSecretKey(keyOrMnemonics.KeyOrMnemonicsValue);
        }
    }
}