namespace MyFramework.Application.Interface
{
    public interface IFileCommand:IBaseCommand
    {
        IFileCommand Title(string title);
        IFileCommand Name(string name);
        IFileCommand Type(string type);
        IFileCommand Content(byte[] content);
        IFileCommand Length(string length);
       
    }
}