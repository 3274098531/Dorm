using System;
using System.Data.Entity;

namespace MyFramework.Application.Interface
{
    public interface IUnitOfWorks  
    {
        DbContext GetDbContext(); 
        void UnBindContext(); 
        void Prepare();
         
    }
}