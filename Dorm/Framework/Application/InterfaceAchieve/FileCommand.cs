using System;
using MyFramework.Application.Interface;
using MyFramework.Domain;

namespace MyFramework.Application.InterfaceAchieve
{
    public class FileCommand<T> : IFileCommand
    {
        public FileCommand(BaseFile  baseFile, IRepository repository)
        {
            BaseFile = baseFile;
            Repository = repository;
        }

        public BaseFile  BaseFile { get; set; }

        public IRepository Repository { get; set; }


        public IFileCommand Title(string title)
        {
             
                BaseFile.Title = title;
              
            return this;
        }

       
        public IFileCommand Name(string name)
        {
             
                BaseFile.Name = name; 
            
            return this;
        }

        public IFileCommand Type(string type)
        {
             
            BaseFile.Type = type;
             
            return this;
        }

        public IFileCommand Content(byte[] content)
        {
          
            BaseFile.Content = content;
            
            return this;
        }

        public IFileCommand Length(string length)
        {
            int len = Int32.Parse(length);
           
                BaseFile.Length = len;
               
            return this;
        }


        public Guid Id { get { return BaseFile.Id; } }
    }
}