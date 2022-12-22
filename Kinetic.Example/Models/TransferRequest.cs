namespace Kinetic.Example.Models;

public class TransferRequest : KeyOrMnemonics
{
    public string Amount { get; set; }
    
    public string Destination { get; set; }
}