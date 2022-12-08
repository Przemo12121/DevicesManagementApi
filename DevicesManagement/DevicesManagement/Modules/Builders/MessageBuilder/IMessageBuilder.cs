using Communication;

namespace DevicesMenagement.Modules.Builders;

/// <summary>
/// Used to build requested message in requested format.
/// </summary>
/// <typeparam name="T">Type of message</typeparam>
/// <typeparam name="U">Type of message content</typeparam>
public interface IMessageBuilder<T, U> where T : IMessage<U>
{
    /// <summary>
    /// Builds a string message.
    /// </summary>
    /// <param name="message">Message instance on which the requested message string will be built.</param>
    /// <returns>Requested message in string format.</returns>
    public string Build(T message);

    /// <summary>
    /// Builds requested instance of message, based on input string.
    /// </summary>
    /// <param name="message">String massege on which the requested instance of message will be built.</param>
    /// <returns>Instance of requested message.</returns>
    public T Build(string message);
}
