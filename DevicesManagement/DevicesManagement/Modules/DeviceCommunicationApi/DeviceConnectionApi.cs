using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    public class DeviceConnectionApi<T> : IDeviceCommunicationApi<T> where T : IMessage<string>
    { 

        public T? Receive(T message)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReceiveAsync(T message)
        {
            throw new NotImplementedException();
        }

        public T? Send(T message)
        {
            throw new NotImplementedException();
        }

        public Task<T> SendAsync(T message)
        {
            throw new NotImplementedException();
        }
    }
}
