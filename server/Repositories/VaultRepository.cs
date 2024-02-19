namespace keeper.Repositories;

public class VaultRepository(IDbConnection db)
{
    private readonly IDbConnection db = db;

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (creatorId ,name, description, img, isPrivate)
        VALUES
        (@creatorId, @name, @description, @img, @isPrivate);

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = LAST_INSERT_ID();
        ";
        Vault vault = db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = @vaultId
        ";
        Vault vault = db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData)
    {
        string sql = @"
        UPDATE vaults SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id;

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = @id
        ";
        Vault updatedVault = db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return updatedVault;
    }

    internal void DeleteVault(int vaultId)
    {
        string sql = @"
        DELETE FROM vaults
        WHERE id = @vaultId
        ";
        db.Execute(sql, new { vaultId });
    }

    internal List<Vault> GetMyVaults(string userId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.creatorId = @userId
        ";
        List<Vault> myVaults = db.Query<Vault, Account, Vault>(sql, (vaults, account) =>
        {
            vaults.Creator = account;
            return vaults;
        }, new { userId }).ToList();
        return myVaults;
    }
}