using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    /// <summary>
    /// Represents the API used for communication with external devices in the network.
    /// </summary>
    /// <typeparam name="T">Type of messages (protocol) used.</typeparam>
    public interface IDeviceCommunicationApi<T> where T : IMessage<string>
    {
        /// <summary>
        /// Sends the given message and returns response if present.
        /// </summary>
        /// <param name="message">Message to send.</param>
        /// <returns>Response, if any.</returns>
        public T? Send(T message);

        /// <summary>
        /// Sends the given message asynchronously and returns response if present.
        /// </summary>
        /// <param name="message">Message to send.</param>
        /// <returns>Response, if any.</returns>
        public Task<T?> SendAsync(T message);

        /// <summary>
        /// Accepts the given message and returns response if present.
        /// </summary>
        /// <param name="message">Message to accept.</param>
        /// <returns>Response, if any.</returns>
        public T? Receive(T message);

        /// <summary>
        /// Accepts the given message asynchronously and returns response if present.
        /// </summary>
        /// <param name="message">Message to accept.</param>
        /// <returns>Response, if any.</returns>
        public Task<T?> ReceiveAsync(T message);
    }
}
