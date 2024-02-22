
namespace keeper.Services;

public class VaultKeepService(VaultKeepRepository repo, VaultService vaultService)
{
    private readonly VaultService vaultService = vaultService;
    private readonly VaultKeepRepository repo = repo;

    internal VaultKeep CreateVaultKeep(VaultKeep VaultKeepData)
    {
        // TODO add user info to check auth, check owner of vault, reference help reviews
        Vault vault = vaultService.GetVaultById(VaultKeepData.VaultId, VaultKeepData.CreatorId);
        if (vault.CreatorId != VaultKeepData.CreatorId) throw new Exception("you don't own that");
        VaultKeep VaultKeep = repo.CreateVaultKeep(VaultKeepData);
        return VaultKeep;
    }
    internal List<VaultKept> GetVaultKeeps(int vaultId, string userId)
    {
        Vault vault = vaultService.GetVaultById(vaultId, userId);
        List<VaultKept> VaultKeep = repo.GetVaultKeeps(vaultId);
        return VaultKeep;
    }
    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = repo.GetVaultKeep(vaultKeepId);
        if (vaultKeep == null) throw new Exception("Vault keep not found");
        return vaultKeep;
    }

    internal string DeleteVaultKeep(int VaultKeepId, string userId)
    {
        // TODO maybve check to see if someone is allowed
        VaultKeep vaultKeep = GetVaultKeepById(VaultKeepId);
        if (vaultKeep.CreatorId != userId) throw new Exception("You did not create this vault keep, therefore you cannot destroy it.");
        repo.DeleteVaultKeep(VaultKeepId);
        return "UnVaultkeepeded, Milord.";
    }
}