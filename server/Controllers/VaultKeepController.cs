using System.Net.Http;

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
            VaultKeep vaultKeep = vaultKeepService.CreateVaultKeep(vaultKeepData);
            return Ok(vaultKeep);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            string message = vaultKeepService.DeleteVaultKeep(vaultKeepId);
            return Ok(message);
        }
        catch (HttpRequestException error)
        {
            return StatusCode((int)error.StatusCode, error.Message);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}