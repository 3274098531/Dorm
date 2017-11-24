using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MyFramework.Domain;

namespace MyFramework.Web.Page
{
    public class PageList<T> : List<T> where T : Entity
    {
        public PageList(int current, int pagesize,int count,IEnumerable<T>entitiees):base(entitiees)
        {
             PageInfo=new PageInfo(current,pagesize,count);
        } 
        public PageInfo PageInfo { get; set; } 
    }
}