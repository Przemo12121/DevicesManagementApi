namespace DevicesMenagement.Database.Models
{
    public class AccessLevel : DatabaseModel, IAccessLevel
    {
        //TODO: change string Name to enum + description
        public string Name { get; set; }

        public override object ToDto()
        {
            return new AccessLevelDto();
        }
    }

    public class AccessLevelDto
    {
    }
}
