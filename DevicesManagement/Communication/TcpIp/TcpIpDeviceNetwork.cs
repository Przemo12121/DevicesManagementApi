namespace Communication.TcpIp;

public class TcpIpDeviceNetwork<T, U> : IDeviceNetwork<T, U> where T : ITcpIpMessage<U>
{
    public void Forward(T message)
    {
        throw new NotImplementedException();
    }

    public Task ForwardAsync(T message)
    {
        throw new NotImplementedException();
    }
}
