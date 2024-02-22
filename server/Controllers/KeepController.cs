namespace keeper.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepController : ControllerBase
{
    private readonly Auth0Provider auth;
    private readonly KeepService keepService;

    public KeepController(Auth0Provider auth, KeepService keepService)
    {
        this.auth = auth;
        this.keepService = keepService;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = keepService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keeps = keepService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = keepService.GetKeepById(keepId);
            keep = keepService.IncreaseViews(keep);
            return Ok(keep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpPut("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep updateKeepData, int keepId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            updateKeepData.CreatorId = userInfo.Id;
            Keep keep = keepService.UpdateKeep(updateKeepData, keepId);
            return Ok(keep);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteKeep(int keepId)
    {
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            string message = keepService.DeleteKeep(keepId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}