namespace Database.Models.Base;
public abstract class CreatableModel : DatabaseModel, ICreatableModel
{
    public DateTime CreatedDate { get; set; }
}
