using Solnet.Rpc.Models;
using Transaction = Kinetic.Sdk.Kinetic.Model.Transaction;

namespace Kinetic.Example.Models;

public class CreateAccountResult
{
    public String SecretKey { get; set; }
    
    public String Mnemonics { get; set; }
    
    public Transaction Transaction { get; set; }
}