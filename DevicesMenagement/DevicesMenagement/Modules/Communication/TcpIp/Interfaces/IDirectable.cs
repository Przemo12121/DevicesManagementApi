namespace DevicesMenagement.Modules.Communication.TcpIp
{
    /// <summary>
    /// Represents messages diractable with Tcp/Ip protocole
    /// </summary>
    public interface IDirectable
    {
        /// <summary>
        /// Message source.
        /// </summary>
        public IDirection Source { get; }

        /// <summary>
        /// Message destination.
        /// </summary>
        public IDirection Destination { get; }
    }
}
