using Communication;

namespace Communication.Builders;

/// <summary>
/// Used to build requested message in requested format.
/// </summary>
/// <typeparam name="T">Type of message</typeparam>
/// <typeparam name="U">Type of message content</typeparam>
public interface IMessageBuilder<T, U> where T : IMessage<U>
{
    public string Build(T message);

    public T Build(string message);
}
