
namespace keeper.Services;

public class VaultKeepService(VaultKeepRepository repo)
{
    private readonly VaultKeepRepository repo = repo;

    internal VaultKeep CreateVaultKeep(VaultKeep VaultKeepData)
    {
        VaultKeep VaultKeep = repo.CreateVaultKeep(VaultKeepData);
        return VaultKeep;
    }
    internal List<VaultKeep> GetVaultKeeps(string userId)
    {
        List<VaultKeep> VaultKeep = repo.GetVaultKeeps(userId);
        return VaultKeep;
    }

    //     internal string DeleteVaultKeep(string VaultKeepId)
    //     {
    //         repo.DeleteVaultKeep(VaultKeepId);
    //         return "UnVaultkeepeded, Milord.";
    //     }
}