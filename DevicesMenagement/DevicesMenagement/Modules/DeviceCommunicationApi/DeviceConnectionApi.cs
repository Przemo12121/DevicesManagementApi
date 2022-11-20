using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    public class DeviceConnectionApi<T> : IDeviceCommunicationApi<T> where T : IMessage<string>
    {
        public T? Send(T message)
        {
            throw new NotImplementedException();
        }

        public T? SendAsync(T message)
        {
            throw new NotImplementedException();
        }
    }
}
