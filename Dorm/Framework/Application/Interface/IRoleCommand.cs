namespace MyFramework.Application.Interface
{
    public interface IRoleCommand:IBaseCommand
    {
        IRoleCommand Name(string name);
        IRoleCommand Discription(string discription);
    }
}
