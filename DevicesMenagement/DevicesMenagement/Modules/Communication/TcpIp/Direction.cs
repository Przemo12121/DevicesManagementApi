namespace DevicesMenagement.Modules.Communication.TcpIp
{
    public class Direction : IDirection
    {
        public string Host { get; }
        public int Port { get; }

        public Direction(string host, int port)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port;
        }

        public override string ToString()
        {
            return $"${this.Host}:${this.Port}";
        }
    }
}
