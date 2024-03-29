using System.Net.Http;

namespace keeper.Controllers;

[ApiController]
[Route("api/vaults")]

public class VaultController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly VaultService vaultService;

    private readonly VaultKeepService vaultKeepService;

    public VaultController(Auth0Provider auth, VaultService vaultService, VaultKeepService vaultKeepService)
    {
        this.auth = auth;
        this.vaultService = vaultService;
        this.vaultKeepService = vaultKeepService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault newVault = vaultService.CreateVault(vaultData);
            return Ok(newVault);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId, string userId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            userId = userInfo?.Id;
            Vault vault = vaultService.GetVaultById(vaultId, userId);
            return Ok(vault);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKept>>> GetVaultKeeps(int vaultId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo?.Id;
            List<VaultKept> vaultKeeps = vaultKeepService.GetVaultKeeps(vaultId, userId);
            return Ok(vaultKeeps);
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




    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int vaultId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            string userId = userInfo.Id;
            Vault vault = vaultService.UpdateVault(vaultData, vaultId, userId);
            return Ok(vault);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int vaultId, string userId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            userId = userInfo.Id;
            string message = vaultService.DeleteVault(vaultId, userId);
            return Ok(message);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}