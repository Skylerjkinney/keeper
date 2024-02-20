namespace keeper.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfileController(ProfilesService profileService, Auth0Provider auth) : ControllerBase
{
    private readonly ProfilesService profilesService = profileService;
    private readonly Auth0Provider auth = auth;

    [HttpGet("{profileId}")]
    public ActionResult<Account> GetProfileById(string profileId)
    {
        try
        {
            Account profile = profilesService.GetProfileById(profileId);
            return Ok(profile);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}