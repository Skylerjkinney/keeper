namespace keeper.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  private readonly VaultService vaultService;
  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultService vaultService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    this.vaultService = vaultService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userEmail = userInfo.Email;
      Account account = _accountService.Edit(editData, userEmail);
      return Ok(account);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetAccountVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string accountId = userInfo.Id;
      List<Vault> vaults = vaultService.GetMyVaults(accountId);
      return Ok(vaults);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
