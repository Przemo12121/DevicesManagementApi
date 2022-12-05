using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    /// <summary>
    /// Object representing additional select constraints.
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    public interface ISearchOptions<T> where T : IDatabaseModel
    {
        /// <summary>
        /// Specifies maximum entities returned by query.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Specifies first entities skipped by query.
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Specifies the order in which entities are returned.
        /// </summary>
        public Func<bool, T>? Order { get; set; }
    }
}
