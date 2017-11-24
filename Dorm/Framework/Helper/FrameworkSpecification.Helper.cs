using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqSpecs;
using MyFramework.Domain;
using NPOI.SS.Formula.Functions;

namespace MyFramework.Helper
{

    #region Find

    /// <summary>
    ///     通过Id查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ById<T> : Specification<T> where T : Entity
    {
        public ById(string id)
        {
            Id = new Guid(id);
        }

        private Guid Id { get; set; }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return x => x.Id == Id;
        }
    }

    /// <summary>
    ///     通过Name查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ByName<T> : Specification<T> where T : Entity
    {
        public ByName(string name)
        {
            Name = name==null?null:name.Trim();
        }

        private string Name { get; set; }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return x => x.Name == Name;
        }
    }

    /// <summary>
    ///     通过Code查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ByCode<T> : Specification<T> where T : Entity
    {
        public ByCode(string code)
        {
            Code = code==null?null:code.Trim();
        }

        private string Code { get; set; }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return x => x.Code == Code;
        }
    }

    public class ByNameOrCode<T>:Specification<T>where T:Entity
    {
        public ByNameOrCode(string nameOrCode)
        {
            NameOrCode = nameOrCode==null?null:nameOrCode.Trim();
        }

        public string NameOrCode { get; set; }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return x => x.Name == NameOrCode || x.Code == NameOrCode;
        }
    }
    #endregion

    #region Search

    public class Search<T>
    {
        public static List<Specification<T>> Specifications;

        public Search()
        {
            Specifications=new List<Specification<T>>();
        } 
         
        public Search<T> Add(string name,Specification<T> specification)
        { 
            if (!string.IsNullOrEmpty(name))
                Specifications.Add(specification);
            return this;
        }

        public Specification<T>[] Result()
        {
            return Specifications.ToArray();
        } 
    }

    #endregion
    #region SpecifiactionExtent
 
    #endregion
    #region Expression Extend

    /// <summary>
    ///     Expression
    /// </summary>
    public static class ExpressionExtend
    {
        public static Expression<Func<T, bool>> ToExpression<T>(this Specification<T>[] specifications) where T : Entity
        {
            Expression<Func<T, bool>> expression = null;
            foreach (var specification in specifications)
            {
                if (specification.IsSatisfiedBy() != null)
                {
                    if (expression == null)
                    {
                        expression = specification.IsSatisfiedBy();
                    }
                    else
                    {
                        expression = expression.And(specification.IsSatisfiedBy());
                    }
                }
            }
            return expression;
        }
       
        public static Expression<Func<T, bool>> True<T>()
        {
            return param => true;
        }

        public static Expression<Func<T, bool>> False<T>() where T : Entity
        {
            return param => false;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) where T : Entity
        {
            return first == null
                ? (second ?? null)
                : (second == null ? first : first.Compose(second, Expression.AndAlso));
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) where T : Entity
        {
            return first == null
                ? (second ?? null)
                : (second == null ? first : first.Compose(second, Expression.OrElse));
        }

        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            Dictionary<ParameterExpression, ParameterExpression> map = first.Parameters
                .Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ParameterRebinder" /> class.
            /// </summary>
            /// <param name="map">The map.</param>
            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            /// <summary>
            ///     Replaces the parameters.
            /// </summary>
            /// <param name="map">The map.</param>
            /// <param name="exp">The exp.</param>
            /// <returns>Expression</returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
                Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }
    }

    #endregion
}