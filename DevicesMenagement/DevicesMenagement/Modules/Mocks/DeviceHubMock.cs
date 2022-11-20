using DevicesMenagement.Modules.DeviceCommunicationApi;
using DevicesMenagement.Modules.Communication.TcpIp;

namespace DevicesMenagement.Modules.Mocks
{
    public class DeviceHubMock : IDeviceCommunicationApi<ITcpIpMessage<string>>
    {
        public ITcpIpMessage<string>? Send(ITcpIpMessage<string> message)
        {
            throw new NotImplementedException();
        }

        public ITcpIpMessage<string>? SendAsync(ITcpIpMessage<string> message)
        {
            throw new NotImplementedException();
        }
    }
}
