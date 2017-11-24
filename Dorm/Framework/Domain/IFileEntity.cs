using System.Collections.Generic;

namespace MyFramework.Domain
{
    public interface IFileEntity<T>
    {
        void AddFile(BaseFile  baseFile);

        void RemoveFile(BaseFile  baseFile);

        ICollection<BaseFile> GetFiles();
    }
}