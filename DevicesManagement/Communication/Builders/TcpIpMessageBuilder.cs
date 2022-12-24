using Communication.TcpIp;

namespace Communication.Builders;

public class TcpIpMessageBuilder<T> : IMessageBuilder<ITcpIpMessage<T>, T>
{
    public string Build(ITcpIpMessage<T> message)
    {
        return "";
    }

    public ITcpIpMessage<T> Build(string message)
    {
        throw new NotImplementedException();
    }
}
