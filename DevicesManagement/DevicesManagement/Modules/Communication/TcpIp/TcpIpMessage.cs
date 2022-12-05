namespace DevicesMenagement.Modules.Communication.TcpIp
{
    public class TcpIpMessage<T> : ITcpIpMessage<T>
    {
        public T Body { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public IDirection Source { get; set; }
        public IDirection Destination { get; set; }

        public TcpIpMessage(T body, IDirection source, IDirection destination)
        {
            Body = body ?? throw new ArgumentNullException("TcpIpMessage body cannot be null");
            Source = source ?? throw new ArgumentNullException("TcpIpMessage source cannot be null");
            Destination = destination ?? throw new ArgumentNullException("TcpIpMessage destination cannot be null");
        }

        public override string ToString() => this.Body.ToString();
    }
}
