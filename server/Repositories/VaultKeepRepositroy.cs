using keeper.Controllers;

namespace keeper.Repositories;

public class VaultKeepRepository(IDbConnection db)
{
    private readonly IDbConnection db = db;
    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@creatorId, @vaultId, @keepId);

        SELECT
        vaultKeeps.*
        FROM
        vaultKeeps
        WHERE id = LAST_INSERT_ID();
        ";
        VaultKeep vaultKeep = db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal List<VaultKeep> GetVaultKeeps(string userId)
    {

    }
}