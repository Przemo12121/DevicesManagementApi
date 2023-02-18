namespace Database.Repositories.Interfaces;

public interface ITransactionableRepository
{
    public Task SaveAsync();
}
