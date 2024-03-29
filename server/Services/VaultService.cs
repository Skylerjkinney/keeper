namespace keeper.Services;

public class VaultService(VaultRepository repo)
{
    private readonly VaultRepository repo = repo;

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = repo.CreateVault(vaultData);
        return vault;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = repo.GetVaultById(vaultId);
        if (vault == null) throw new Exception("No vault there");
        if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception("vault is private and you don't own it");
        else
        {
            return vault;
        }
    }
    internal List<Vault> GetMyVaults(string userId)
    {
        if (userId != null)
        {
            List<Vault> myVaults = repo.GetMyVaults(userId);
            return myVaults;
        }
        else { throw new Exception("Log in to continue"); }
    }

    internal Vault UpdateVault(Vault vaultData, int vaultId, string userId)
    {
        Vault originalVault = GetVaultById(vaultId, userId) ?? throw new Exception("Item not found.");
        if (originalVault.CreatorId == userId)
        {
            originalVault.Img = vaultData.Img?.Length > 0 ? vaultData.Img : originalVault.Img;
            originalVault.Name = vaultData.Name?.Length > 0 ? vaultData.Name : originalVault.Name;
            originalVault.IsPrivate = vaultData.IsPrivate ? vaultData.IsPrivate : originalVault.IsPrivate;
            originalVault.Description = vaultData.Description?.Length > 0 ? vaultData.Description : originalVault.Description;
            Vault newVault = repo.UpdateVault(originalVault);
            return newVault;
        }
        else
        {
            throw new Exception("You are not authorized");
        }
    }

    internal string DeleteVault(int vaultId, string userId)
    {
        Vault foundVault = GetVaultById(vaultId, userId);
        if (foundVault.CreatorId == userId)
        {
            repo.DeleteVault(vaultId);
            return $"{foundVault.Name} was deleted.";
        }
        else
        { throw new Exception("You are not authorized"); }
    }

}