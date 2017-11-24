using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using LinqSpecs;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using MyFramework.Domain;
using MyFramework.Helper;
using MyFramework.Web.Page;

namespace MyFramework.Application.InterfaceAchieve
{
    [IocRegister]
    public class Repository : IRepository
    {
        public Repository(IUnitOfWorks unitOfWorks)
        {
            DbContext = unitOfWorks.GetDbContext();
        } 
        public DbContext DbContext { get; set; } 
        #region
        public void Save<T>(T entity)
            where T : Entity
        {
            string username = IoC.Get<IHttpService>().GetInlineAccountUserName();
            if (entity.Id != Guid.Empty)
            {
                entity.UpdateTime = DateTime.Now;
                entity.UpdateUserName = username;
            }
            else
            {
                entity.Id = Guid.NewGuid();
                entity.CreateTime = DateTime.Now;
                entity.CreateUserName = username;
                DbContext.Set<T>().Add(entity);
            }

            
        }

        public void Save<TEntity>(IList<TEntity> entities)
            where TEntity : Entity
        {
            string username = IoC.Get<IHttpService>().GetInlineAccountUserName();
            foreach (TEntity entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.UpdateUserName = username;
                entity.CreateUserName = username;
            }
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public T GetById<T>(string id) where T : Entity
        {
            return GetById<T>(new Guid(id));
        }

         

        public void Remove<T>(params Specification<T>[] specifications) where T : Entity
        {
            Expression<Func<T, bool>> specification = specifications.ToExpression();
            DbContext.Set<T>().RemoveRange(DbContext.Set<T>().Where(specification));
        }

        public void Remove<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            DbContext.Set<T>().RemoveRange(DbContext.Set<T>().Where(expression));
        }

        

        public T GetById<T>(Guid id) where T : Entity
        {
            return DbContext.Set<T>().Find(id);
        } 
        public T GetOne<T>(params Specification<T>[] specifications) where T : Entity
        {
            IQueryable<T> entities;
            if (!specifications.Any())
            {
                entities = DbContext.Set<T>();
            }
            else
            {
                Expression<Func<T, bool>> specification = specifications.ToExpression();
                entities = DbContext.Set<T>()
                    .Where(specification);
            }
            if (entities.Count() > 1)
                throw new Exception("太多行");
            if (!entities.Any())
                throw new Exception("没有此行");
            return entities .Single();
        }


        public PageList<T> GetAllOrderbyDesc<T>(int? index, int? pagesize, Expression<Func<T, bool>> expression = null, Expression<Func<T, object>> @orderby = null,
            params Expression<Func<T, object>>[] thenbys) where T : Entity
        {
            var entitys = GetAll(expression);
            if (orderby != null)
            {
                switch (thenbys.Count())
                {
                    case 0:
                        entitys = entitys.OrderBy(orderby);break;
                    case 1:
                        entitys = entitys.OrderBy(orderby).ThenBy(thenbys[0]);break;
                    case 2:
                        entitys = entitys.OrderBy(orderby).ThenBy(thenbys[0]).ThenBy(thenbys[1]);break;
                    case 3:
                        entitys = entitys.OrderBy(orderby).ThenBy(thenbys[0]).ThenBy(thenbys[1]).ThenBy(thenbys[2]);break;
                } 
            }
            int currentsize = index ?? 1;
            int size = pagesize ?? int.Parse(IoC.Get<IConfig>().GetValue(FrameworkKeys.PageSize));
            
            int count = entitys.Count(); 
            return new PageList<T>(currentsize,size,count,entitys.Skip((currentsize-1)*size).Take(size).ToList());
        }
        public PageList<T> GetAllOrderbyAsc<T>(int? index, int? pagesize, Expression<Func<T, bool>> expression = null, Expression<Func<T, object>> @orderby = null,
            params Expression<Func<T, object>>[] thenbys) where T : Entity
        {
            var entitys = GetAll(expression);
            if (orderby != null)
            {
                switch (thenbys.Count())
                {
                    case 0:
                        entitys = entitys.OrderByDescending(orderby); break;
                    case 1:
                        entitys = entitys.OrderByDescending(orderby).ThenByDescending(thenbys[0]); break;
                    case 2:
                        entitys = entitys.OrderByDescending(orderby).ThenByDescending(thenbys[0]).ThenByDescending(thenbys[1]); break;
                    case 3:
                        entitys = entitys.OrderByDescending(orderby).ThenByDescending(thenbys[0]).ThenByDescending(thenbys[1]).ThenByDescending(thenbys[2]); break;
                }
            }
            int currentsize = index ?? 1;
            int size = pagesize ?? int.Parse(IoC.Get<IConfig>().GetValue(FrameworkKeys.PageSize));

            int count = GetCount(expression);
            return new PageList<T>(currentsize, size, count, entitys.Skip((currentsize - 1) * size).Take(size).ToList<T>());
        }
        public int GetCount<T>(params Specification<T>[] specifications) where T : Entity
        {
            return GetAll(specifications).Count();
        }


        public bool IsExisted<T>(params Specification<T>[] specifications) where T : Entity
        {
            return GetAll(specifications).Any();
        }

        public bool IsExisted<T>(Expression<Func<T,bool>> specifications) where T : Entity
        {
            return DbContext.Set<T>().Any(specifications);
        }

        public void Submit()
        {
            DbContext.SaveChanges();
        }


        public IQueryable<T> GetAll<T>(params Specification<T>[] specifications) where T : Entity
        {
            if (!specifications.Any()) return DbContext.Set<T>().AsNoTracking();
            Expression<Func<T, bool>> specification = specifications.ToExpression();
            return DbContext.Set<T>()
                .Where(specification).AsNoTracking();
        }



        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            if (expression == null) return DbContext.Set<T>().AsNoTracking();
            return DbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> GetAllOrderAsc<T>(Expression<Func<T, bool>> expression, Expression<Func<T, object>> @orderby=null, params Expression<Func<T, object>>[] thenby) where T : Entity
        {
             var entitys= GetAll(expression).OrderBy(@orderby);
            foreach (var expression1 in thenby)
            {
                entitys = entitys.ThenBy(expression1);
            }
            return entitys;
        }
         

        public T GetOne<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            IQueryable<T> entities;
            if (expression == null) entities = DbContext.Set<T>();
            else
            {
                entities = DbContext.Set<T>()
                    .Where(expression);
            }
            if (entities.Count() > 1)
                throw new Exception("太多行");
            if (!entities.Any())
                throw new Exception("没有此行");
            return entities.Single();
        }

        


        public int GetCount<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            return GetAll(expression).Count();
        }
    }
        #endregion
}