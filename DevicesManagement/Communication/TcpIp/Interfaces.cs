using Communication;

namespace Communication.TcpIp;

/// <summary>
/// Represents Tcp/Ip message.
/// </summary>
/// <typeparam name="T">Object that can be represented as message.</typeparam>
public interface ITcpIpMessage<T> : IMessage<T>, IDirectable
{
}

/// <summary>
/// Represents messages diractable with Tcp/Ip protocole
/// </summary>
public interface IDirectable
{
    public IDirection Source { get; }

    public IDirection Destination { get; }
}

/// <summary>
/// Represents Tcp/Ip direction.
/// </summary>
public interface IDirection
{
    public string Host { get; }

    public int Port { get; }
}
