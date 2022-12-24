namespace Communication.TcpIp;

public record TcpIpMessage<T>(T Body, DateTime CreatedAt, IDirection Source, IDirection Destination) : ITcpIpMessage<T>
{
    public override string ToString() => Body?.ToString() ?? throw new ArgumentNullException();
}
