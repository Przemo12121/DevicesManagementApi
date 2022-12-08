namespace Communication;

public record DeviceCommunicationApi<T, U> (IMessageListener<T, U> MessageListener, IDeviceNetwork<T, U> DeviceNetwork) 
    : IDeviceCommunicationApi<T, U>
    where T : IMessage<U>
{
    public void Send(T message)
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(T message)
    {
        throw new NotImplementedException();
    }
}
