namespace DevicesMenagement.Modules.Communication.TcpIp
{
    public interface IDirectable
    {
        public IDirection Source { get; }
        public IDirection Destination { get; }
    }
}
