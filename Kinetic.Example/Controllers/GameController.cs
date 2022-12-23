using Kinetic.Example.Models;
using Kinetic.Sdk;
using Kinetic.Sdk.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kinetic.Example.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private KineticSdk KineticSdk { get; set; }

    public GameController(ILogger<GameController> logger, KineticSdk kineticSdk)
    {
        KineticSdk = kineticSdk;
    }

    [HttpGet("CreateNewAccount")]
    public async Task<IActionResult> CreateNewAccount()
    {
        Keypair keypair = Keypair.Random();
        var transaction = await KineticSdk.CreateAccount(keypair, commitment: Commitment.Finalized);

        var createAccountResult = new CreateAccountResult
        {
            SecretKey = keypair.SecretKey,
            Mnemonics = keypair.Mnemonic.ToString(),
            Transaction = transaction
        };
        return Ok(createAccountResult);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] KeyOrMnemonics keyOrMnemonics)
    {
        Keypair keypair = Utility.GetKeyPair(keyOrMnemonics);

        var loginResult = new LoginResult
        {
            SecretKey = keypair.SecretKey,
            Mnemonics = keypair.Mnemonic?.ToString() ?? null,
        };

        return Ok(loginResult);
    }
}