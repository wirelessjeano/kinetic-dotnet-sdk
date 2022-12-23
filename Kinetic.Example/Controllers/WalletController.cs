using Kinetic.Example.Models;
using Kinetic.Sdk;
using Kinetic.Sdk.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kinetic.Example.Controllers;

[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    private KineticSdk KineticSdk { get; set; }
    
    private ILogger<WalletController> Logger  { get; set; }
    public WalletController(ILogger<WalletController> logger, KineticSdk kineticSdk)
    {
        Logger = logger;
        KineticSdk = kineticSdk;
    }
    
   [HttpPost("GetBalance")]
    public async Task<IActionResult> GetBalance([FromBody] KeyOrMnemonics keyOrMnemonics)
    {
        Keypair keypair = Utility.GetKeyPair(keyOrMnemonics);

        var balanceResult = await _getBalance(keypair);
        return Ok(balanceResult);
    }

    

    [HttpPost("GetTokenAccounts")]
    public async Task<IActionResult> GetTokenAccounts([FromBody] KeyOrMnemonics keyOrMnemonics)
    {
        Keypair keypair = Utility.GetKeyPair(keyOrMnemonics);
        
        var accounts = await KineticSdk.GetTokenAccounts( account: keypair.PublicKey );

        return Ok(accounts);

    }

    [HttpPost("GetHistory")]
    public async Task<IActionResult> GetHistory([FromBody] KeyOrMnemonics keyOrMnemonics)
    {
        Keypair keypair = Utility.GetKeyPair(keyOrMnemonics);
        
        var history =  await KineticSdk.GetHistory( account: keypair.PublicKey );
        return Ok(history);
    }
    
    [HttpPost("RequestAirdrop")]
    public async Task<IActionResult> RequestAirdrop([FromBody] KeyOrMnemonics keyOrMnemonics)
    {
        Keypair keypair = Utility.GetKeyPair(keyOrMnemonics);
        
        try
        {
            var airDropResponse = await KineticSdk.RequestAirdrop(
                account: keypair.PublicKey,
                amount: "1000"
            );
            
            var balanceResult = await _getBalance(keypair);
            return Ok(new {balanceResult, airDropResponse});
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Requested an airdrop but it failed.");
            
            throw;
        }

    }

    [HttpPost("MakeTransfer")]
    public async Task<IActionResult> MakeTransfer([FromBody] TransferRequest transferRequest)
    {
        Keypair keypair = Utility.GetKeyPair(transferRequest);
        

        try
        {
            
            var transaction = await KineticSdk.MakeTransfer(
                amount: transferRequest.Amount,
                destination: transferRequest.Destination,
                owner: keypair,
                commitment: Commitment.Finalized,
                senderCreate: true
            );
           
            var balanceResult = await _getBalance(keypair);
            return Ok(new {balanceResult, transaction});
        }
        catch (Exception e)
        {
            Logger.LogError(e, " - Invalid destination - ");
            
            throw;
        }

    }
    
    private async Task<BalanceResult> _getBalance(Keypair keypair)
    {
        var balance = await KineticSdk.GetBalance(account: keypair.PublicKey);

        var balanceResult = new BalanceResult
        {
            Balance = (float.Parse(balance.Balance) / Math.Pow(10, 5)).ToString("0.00") + " KIN"
        };

        return balanceResult;
    }

}