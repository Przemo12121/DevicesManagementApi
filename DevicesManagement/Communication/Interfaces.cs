namespace Communication;

/// <summary>
/// Represents communication message.
/// </summary>
/// <typeparam name="T">Any object that can be converted to string.</typeparam>
public interface IMessage<T>
{
    public DateTime CreatedAt { get; init; }

    public T Body { get; init; }
}

/// <summary>
/// Represents the API used for communication with external devices in the network.
/// </summary>
/// <typeparam name="T">Type of messages (protocol) used.</typeparam>
public interface IDeviceCommunicationApi<T, U> where T : IMessage<U>
{
    public void Send(T message);

    public Task SendAsync(T message);

    public IMessageListener<T, U> MessageListener { get; init; }
}

/// <summary>
/// Represents the communication network.
/// </summary>
/// <typeparam name="T">Type of message (protocol) used.</typeparam>
/// <typeparam name="U">Type of content carried by messages.</typeparam>
public interface IDeviceNetwork<T, U> where T : IMessage<U>
{
    public void Forward(T message);
    public Task ForwardAsync(T message);
}

public interface IMessageListener<T, U> where T : IMessage<U>
{
    public void HandleMessage(T message);
}