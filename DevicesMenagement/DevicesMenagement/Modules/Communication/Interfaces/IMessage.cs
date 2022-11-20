namespace DevicesMenagement.Modules.Communication
{
    public interface IMessage<T> : IStringable
    {
        public DateTime CreatedAt { get; }
        public T Body { get; set; }
    }
}
