using Solnet.Rpc.Models;
using Transaction = Kinetic.Sdk.Kinetic.Model.Transaction;

namespace Kinetic.Example.Models;

public class LoginResult
{
    public String SecretKey { get; set; }
    
    public string? Mnemonics { get; set; }
}