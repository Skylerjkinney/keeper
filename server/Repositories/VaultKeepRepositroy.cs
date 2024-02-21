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

    internal List<VaultKept> GetVaultKeeps(int vaultId)
    {
        string sql = @"
        SELECT
        keeps.*,
        vaultKeeps.*,
        accounts.*
        FROM vaultKeeps
        JOIN keeps ON vaultKeeps.keepId = keeps.id
        JOIN accounts ON vaultKeeps.creatorId = accounts.id
        WHERE vaultKeeps.vaultId = @vaultId;
        ";

        List<VaultKept> vaultKepts = db.Query<VaultKept, VaultKeep, Account, VaultKept>(sql, (vaultKept, vaultKeep, account) =>
        {
            vaultKept.VaultKeepId = vaultKeep.Id;
            vaultKept.Creator = account;
            return vaultKept;
        }, new { vaultId }).ToList();
        return vaultKepts;
    }

    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = @"
        DELETE FROM vaultKeeps
        WHERE id = @vaultKeepId
        ";
        db.Execute(sql, new { vaultKeepId });
    }
}