using Database.Models.Base;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.Factories.SearchOptions;

public interface ISearchOptionsFactory<T, U> where T : IDatabaseModel
{
    ISearchOptions<T, U> From(PaginationRequest request);
}
