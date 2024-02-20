namespace keeper.Services;

public class ProfilesService
{
    private readonly AccountsRepository _repo;

    public ProfilesService(AccountsRepository repo)
    {
        _repo = repo;
    }

    internal Account GetProfileById(string profileId)
    {
        Account profile = _repo.GetById(profileId);
        if (profile == null) throw new Exception($"No profile at: {profileId}");
        return profile;
    }
    internal List<Keep> GetProfileKeeps(string profileId)
    {
        List<Keep> keeps = _repo.GetProfileKeeps(profileId);
        return keeps;
    }
    internal List<Vault> GetProfileVaults(string profileId)
    {
        List<Vault> vaults = _repo.GetProfileVaults(profileId);
        return vaults;
    }
}