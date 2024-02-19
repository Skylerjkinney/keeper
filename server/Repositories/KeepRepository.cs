namespace keeper.Repositories;

public class KeepRepository(IDbConnection db)
{
    private readonly IDbConnection db = db;

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (creatorId ,name, description, img, views)
        VALUES
        (@creatorId, @name, @description, @img, @views);

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();
        ";
        Keep keep = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            keep.Kept = 0;
            return keep;
        }, keepData).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id";
        List<Keep> keeps = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @keepId
        ";
        Keep keep = db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            keep.Views++;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal Keep UpdateKeep(Keep updateData)
    {
        string sql = @"
        UPDATE keeps SET
        name = @name,
        description = @description,
        img = @img,
        views = @views
        WHERE id = @id;

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @id
        ";
        Keep keeps = db.Query<Keep, Account, Keep>(sql, (keeps, account) =>
        {
            keeps.Creator = account;
            return keeps;
        }, updateData).FirstOrDefault();
        return keeps;
    }

    internal void DeleteKeep(int keepId)
    {
        string sql = @"
        DELETE FROM keeps
        WHERE id = @keepId
        ";
        db.Execute(sql, new { keepId });
    }
}