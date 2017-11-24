using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting;
using LinqSpecs;
using MyFramework.Domain;
using MyFramework.Web.Page;

namespace MyFramework.Application.Interface
{
    public interface IRepository
    {
         
        T GetById<T>(Guid id) where T : Entity;
        T GetById<T>(string id) where T : Entity;
      
        void Remove<T>(params Specification<T>[] specification) where T : Entity;
        void Remove<T>(Expression<Func<T, bool>> expressions) where T : Entity;
        void Save<T>(T entity) where T : Entity;
        void Save<T>(IList<T> entities) where T : Entity;
        IQueryable<T> GetAll<T>(params Specification<T>[] specifications) where T : Entity;
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression) where T : Entity;
         
        T GetOne<T>(params Specification<T>[] specifications) where T : Entity;
        T GetOne<T>(Expression<Func<T, bool>> expression) where T : Entity;
        PageList<T> GetAllOrderbyAsc<T>(int?index,int?pagesize,Expression<Func<T,bool>> expression=null,Expression<Func<T,object>> orderby=null,params Expression<Func<T,object>>[] thenbys) where T : Entity;
        PageList<T> GetAllOrderbyDesc<T>(int?index,int?pagesize,Expression<Func<T,bool>> expression=null,Expression<Func<T,object>> orderby=null,params Expression<Func<T,object>>[] thenbys) where T : Entity;
        int GetCount<T>(params Specification<T>[] specifications) where T : Entity;
        int GetCount<T>(Expression<Func<T, bool>> expression) where T : Entity;
        bool IsExisted<T>(params Specification<T>[] specifications) where T : Entity;
        bool IsExisted<T>(Expression<Func<T,bool>> specifications) where T : Entity;
        void Submit();
    }
}