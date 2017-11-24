namespace DormitoryManagement.Application
{
    public interface IFileCommand
    {
        IFileCommand Title(string title);
        IFileCommand Name(string name);
        IFileCommand Type(string type);
        IFileCommand Content(byte[] content);
        IFileCommand Length(string length);
       
    }
}