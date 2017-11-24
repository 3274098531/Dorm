using System;
using System.Collections.Generic;
using System.Linq;
using DormitoryManagement.Application;
using LinqSpecs;
using MyFramework.Domain;

namespace MyFramework.Application
{
    public interface IRepository : IDependency 
    {
        T GetById<T>(Guid id) where T : Entity;

        IQueryable<T> All<T>() where T : Entity;

        
        void Remove<T>(T entity) where T : Entity;
        void Remove<T>(IEnumerable<T> entity) where T : Entity;

        void Save<T>(T entity)where T:Entity;

        void Save<T>(IEnumerable<T> entities) where T : Entity;



        IQueryable<T> GetAll<T>(params LinqSpecs.Specification<T>[] specifications) where T : Entity;
        IQueryable<T> GetAll<T>(List<LinqSpecs.Specification<T>> specifications) where T : Entity;


        T GetOne<T>(params LinqSpecs.Specification<T>[] specifications)where T:Entity;
        T GetOne<T>(List<LinqSpecs.Specification<T>> specifications) where T : Entity;

        int GetCount<T>(params LinqSpecs.Specification<T>[] specifications) where T : Entity;
        int GetCount<T>(List<LinqSpecs.Specification<T>> specifications) where T : Entity;

        bool IsExisted<T>(params LinqSpecs.Specification<T>[] specifications) where T : Entity;


        void Commit();

    }
}