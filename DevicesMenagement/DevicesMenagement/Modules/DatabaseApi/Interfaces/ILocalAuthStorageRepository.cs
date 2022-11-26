using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface ILocalAuthStorageRepository : IDisposable, IUpdatableRepository<IUser>
    {
        /// <summary>
        /// Tries to find the employee based on given eid.
        /// </summary>
        /// <param name="eid">Requested employee's Employee Identifier.</param>
        /// <returns>Employee found.</returns>
        IUser FindByEmployeeId(string eid);

        /// <summary>
        /// Returns list of all employees registered in the system.
        /// </summary>
        /// <returns>List of employees.</returns>
        List<IUser> FindAllEmployees();

        /// <summary>
        /// Returns list of all employees registered in the system, that matched query with given options.
        /// </summary>
        /// <param name="options">Options to build the query with.</param>
        /// <returns>List of employees matched by the query.</returns>
        List<IUser> FindAllEmployees(ISearchOptions<IUser> options);

        /// <summary>
        /// Returns list of all admins registered in the system.
        /// </summary>
        /// <returns>List of admins.</returns>
        List<IUser> FindAllAdmins();

        /// <summary>
        /// Returns list of all admins registered in the system, that matched query with given options.
        /// </summary>
        /// <param name="options">Options to build the query with.</param>
        /// <returns>List of admins matched by the query.</returns>
        List<IUser> FindAllAdmins(ISearchOptions<IUser> options);
    }
}
