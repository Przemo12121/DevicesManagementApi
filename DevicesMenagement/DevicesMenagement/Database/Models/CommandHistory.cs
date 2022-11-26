namespace DevicesMenagement.Database.Models
{
    public class CommandHistory : CreatableModel, ICommandHistory
    {
        public Command Command { get; set; }

        public override CommandHistoryDto ToDto()
        {
            return new CommandHistoryDto();
        }
    }

    public class CommandHistoryDto
    {

    }
}
