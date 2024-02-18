namespace keeper.Services;

public class KeepService(KeepRepository repo)
{
    private readonly KeepRepository repo = repo;

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = repo.CreateKeep(keepData);
        return keep;
    }

    internal List<Keep> GetKeeps()
    {
        List<Keep> keeps = repo.GetKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = repo.GetKeepById(keepId);
        if (keep == null) throw new Exception("Invalid Keep");
        return keep;
    }

    internal Keep UpdateKeep(Keep updateData, int keepId)
    {
        Keep originalKeep = GetKeepById(keepId);
        originalKeep.Img = updateData.Img?.Length > 0 ? updateData.Img : originalKeep.Img;
        originalKeep.Description = updateData.Description?.Length > 0 ? updateData.Description : originalKeep.Description;
        originalKeep.Name = updateData.Name?.Length > 0 ? updateData.Name : originalKeep.Name;
        Keep updatedKeep = repo.UpdateKeep(originalKeep);
        return updatedKeep;
    }

    internal string DeleteKeep(int keepId, string userId)
    {
        Keep keepToDelete = GetKeepById(keepId);
        if (keepToDelete.CreatorId == userId)
        {
            repo.DeleteKeep(keepId);
            return $"{keepToDelete.Name} removed";
        }
        else { throw new Exception("You are not the owner"); }
    }
}