
namespace keeper.Services;

public class VaultKeepService(VaultKeepRepository repo, VaultService vaultService)
{
    private readonly VaultService vaultService = vaultService;
    private readonly VaultKeepRepository repo = repo;

    internal VaultKeep CreateVaultKeep(VaultKeep VaultKeepData)
    {
        VaultKeep VaultKeep = repo.CreateVaultKeep(VaultKeepData);
        return VaultKeep;
    }
    internal List<VaultKept> GetVaultKeeps(int vaultId)
    {
        List<VaultKept> VaultKeep = repo.GetVaultKeeps(vaultId);
        return VaultKeep;
    }

    internal string DeleteVaultKeep(int VaultKeepId)
    {
        repo.DeleteVaultKeep(VaultKeepId);
        return "UnVaultkeepeded, Milord.";
    }
}