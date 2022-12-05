namespace DevicesMenagement.Modules.Communication
{
    /// <summary>
    /// Represents communication message.
    /// </summary>
    /// <typeparam name="T">Any object that can be converted to string.</typeparam>
    public interface IMessage<T>
    {
        /// <summary>
        /// Message creation date.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Message actual content.
        /// </summary>
        public T Body { get; set; }
    }
}
