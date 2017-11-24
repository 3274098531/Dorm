using System;
using DormitoryManagement.Domain;
using MyFramework.Application;
using MyFramework.Application.Interface;
using MyFramework.Domain;

namespace DormitoryManagement.Application.Imp
{
    public class FileCommand : IFileCommand
    {
        public FileCommand(File file, IRepository repository)
        {
            File = file;
            Repository = repository;
        }

        public File File { get; set; }

        public IRepository Repository { get; set; }


        public IFileCommand Title(string title)
        {
             
                File.Title = title;
              
            return this;
        }

       
        public IFileCommand Name(string name)
        {
             
                File.Name = name; 
            
            return this;
        }

        public IFileCommand Type(string type)
        {
             
            File.Type = type;
             
            return this;
        }

        public IFileCommand Content(byte[] content)
        {
          
            File.Content = content;
            
            return this;
        }

        public IFileCommand Length(string length)
        {
            int len = Int32.Parse(length);
           
                File.Length = len;
               
            return this;
        }


        public Guid Id { get { return File.Id; } }
    }
}