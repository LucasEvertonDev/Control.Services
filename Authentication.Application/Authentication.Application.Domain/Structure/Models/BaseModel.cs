namespace Authentication.Application.Domain.Structure.Models;
public abstract class BaseModel
{
    public abstract BaseModel FromEntity(IEntity entity);
}
