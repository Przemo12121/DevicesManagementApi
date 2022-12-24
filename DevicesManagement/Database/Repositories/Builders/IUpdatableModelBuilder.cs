using Database.Models.Base;

namespace Database.Repositories.Builders;

/// <summary>
/// Builder creating editable database entities.
/// </summary>
/// <typeparam name="T">Updatable database entity.</typeparam>
public interface IUpdatableModelBuilder<T> where T : IUpdatableModel
{
    public T Build();
}
