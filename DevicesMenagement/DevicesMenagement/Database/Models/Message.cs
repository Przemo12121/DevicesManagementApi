namespace DevicesMenagement.Database.Models
{
    public class Message : CreatableModel, IMessage
    {
        public string Content { get; set ; }
        public string From { get; set; }
        public string To { get; set; }

        public override MessageDtp ToDto()
        {
            return new MessageDtp();
        }
    }

    public class MessageDtp
    {

    }
}
