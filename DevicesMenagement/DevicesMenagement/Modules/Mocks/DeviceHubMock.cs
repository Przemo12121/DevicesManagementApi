using DevicesMenagement.Modules.DeviceCommunicationApi;
using DevicesMenagement.Modules.Communication.TcpIp;

namespace DevicesMenagement.Modules.Mocks
{
    public class DeviceHubMock
    {
        public ITcpIpMessage<string>? Forward(ITcpIpMessage<string> message)
        {
            throw new NotImplementedException();
        }

        public ITcpIpMessage<string>? ForwardAsync(ITcpIpMessage<string> message)
        {
            throw new NotImplementedException();
        }
    }
}
