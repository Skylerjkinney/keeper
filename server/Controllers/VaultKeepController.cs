namespace keeper.Controllers;
[ApiController]
[Route("api/vaultkeeps")]

public class VaultKeepsController : ControllerBase
{
    private readonly VaultKeepService vaultKeepService;
    private readonly Auth0Provider auth;

    public VaultKeepsController(VaultKeepService vaultKeepService, Auth0Provider auth)
    {
        this.vaultKeepService = vaultKeepService;
        this.auth = auth;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = vaultKeepService.CreateVaultKeep(vaultKeepData);
            return Ok(vaultKeep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}