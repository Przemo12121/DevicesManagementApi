using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    public interface IDeviceCommunicationApi<T> where T : IMessage<string>
    {
        public T? Send(T message);
        public T? SendAsync(T message);
    }
}
