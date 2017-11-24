using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Machine.Specifications;
using MyFramework.Application.Interface;
using MyFramework.Attribute;
using Rhino.Mocks;

namespace MyFramework.Web.测试
{
    public class BaseActionSpec
    {
        protected static IActionSubject subject;
        protected static ActionResult result;
        protected static IRepository repository;
         
        protected Establish FirstContext =
            () =>
            { 
                repository = A<IRepository>();
            };

        protected Cleanup Ofter =
            () => A<IUnitOfWorks>().UnBindContext();


        protected static T A<T>()
        {
            return IoC.Get<T>();
        }

        protected static IActionSubject Action<TController>(Expression<Func<TController, ActionResult>> action)
            where TController : Controller
        {
            A<IUnitOfWorks>().UnBindContext();
            return new ActionSubject<TController>(A<TController>(), action);
        }

        protected static T AsModel<T>(ActionResult result)
        {
            return (T) (((ViewResult) result).ViewData.Model);
        }

        protected static IEnumerable<T> AsModels<T>(ActionResult result)
        {
            return (IEnumerable<T>) (((ViewResult) result).ViewData.Model);
        }
        public static HttpPostedFileBase 文件( string content)
        {
            var stream = new MemoryStream(Encoding.Default.GetBytes(content));
            var file = MockRepository.GenerateMock<HttpPostedFileBase>();
            file.Stub(x => x.ContentLength).Return((int)stream.Length);
            file.Stub(x => x.InputStream).Return(stream); 
            return file;
        }
    }
}