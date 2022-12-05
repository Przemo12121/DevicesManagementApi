
using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public class CommandsRepository : ICommandsRepository
    {
        public void Delete(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void Update(IUpdatableModelBuilder<ICommand> builder)
        {
            throw new NotImplementedException();
        }
    }
}
