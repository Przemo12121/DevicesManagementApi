using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    /// <summary>
    /// Represents the communication network.
    /// </summary>
    /// <typeparam name="T">Type of message (protocol) used.</typeparam>
    /// <typeparam name="U">Type of content carried by messages.</typeparam>
    public interface IDeviceNetwork<T, U> where T : IMessage<U>
    {
        public T? Forward(T message);
        public T? ForwardAsync(T message);
    }
}
