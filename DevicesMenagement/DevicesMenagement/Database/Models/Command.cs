namespace DevicesMenagement.Database.Models
{
    public class Command : UpdatableModel, ICommand
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Body {  get; set; }

        public override CommandDto ToDto()
        {
            return new CommandDto();
        }
    }

    public class CommandDto
    {

    }
}
