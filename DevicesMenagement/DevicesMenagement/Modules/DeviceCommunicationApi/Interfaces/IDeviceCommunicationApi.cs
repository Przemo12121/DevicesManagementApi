using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    public interface IUserCommunicationApi<T> where T : IMessage<string>
    {
        public T? Send(T message);
        public T? SendAsync(T message);
    }
}
