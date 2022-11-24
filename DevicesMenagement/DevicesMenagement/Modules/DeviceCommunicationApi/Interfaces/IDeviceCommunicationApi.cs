using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    public interface IDeviceCommunicationApi<T> where T : IMessage<string>
    {
        public T? Send(T message);
        public Task<T> SendAsync(T message);
        public T? Receive(T message);
        public Task<T> ReceiveAsync(T message);
    }
}
