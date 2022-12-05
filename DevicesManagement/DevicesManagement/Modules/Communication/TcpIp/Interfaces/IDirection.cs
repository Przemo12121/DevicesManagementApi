namespace DevicesMenagement.Modules.Communication.TcpIp
{
    /// <summary>
    /// Represents Tcp/Ip direction.
    /// </summary>
    public interface IDirection
    {
        /// <summary>
        /// Direction's host.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Direction's port.
        /// </summary>
        public int Port { get; }
    }
}
