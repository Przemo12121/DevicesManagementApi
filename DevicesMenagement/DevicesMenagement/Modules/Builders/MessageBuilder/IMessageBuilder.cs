using DevicesMenagement.Modules.Communication;

namespace DevicesMenagement.Modules.Builders;

public interface IMessageBuilder<T, U> where T : IMessage<U>
{
    public string Build(T message);
    public T Build(string message);
}
