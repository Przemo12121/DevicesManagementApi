using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.DeviceCommunicationApi
{
    public interface IUserHub<T, U> where T : IMessage<U>
    {
        public T? Forward(T message);
        public T? ForwardAsync(T message);
    }
}
