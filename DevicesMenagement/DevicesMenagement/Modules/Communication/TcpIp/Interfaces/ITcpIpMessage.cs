namespace DevicesMenagement.Modules.Communication.TcpIp
{
    /// <summary>
    /// Represents Tcp/Ip message.
    /// </summary>
    /// <typeparam name="T">Object that can be represented as message.</typeparam>
    public interface ITcpIpMessage<T> : IMessage<T>, IDirectable
    {
    }
}
