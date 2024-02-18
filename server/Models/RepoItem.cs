namespace keeper.Models;

public abstract class RepoItem<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}