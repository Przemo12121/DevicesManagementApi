using Database.Models.Base;

namespace Database.Repositories.Builders;

/// <summary>
/// Builder creating insertable database entities.
/// </summary>
/// <typeparam name="T">Creatable database entity.</typeparam>
public interface ICreatableModelBuilder<T> where T : ICreatableModel
{
    public T Build();
}
