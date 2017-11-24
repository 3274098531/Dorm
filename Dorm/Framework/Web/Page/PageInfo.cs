using System;

namespace MyFramework.Web.Page
{
    public struct PageInfo
    {
        public PageInfo(int index, int pagesize, int count, string actionName=null)
            : this()
        {
            CurrentIndex = index;
            PageSize = pagesize;
            TotalCount = count;
            ActionName = actionName ?? "Index";
        }

        public string SearchKeyWord { get; set; }

        /// <summary>
        ///     点击分页是指向 Action 的名字 根据具体需要而定
        /// </summary>
        public string ActionName { get; set; }

        public int TotalCount { get; set; }

        public int CurrentIndex { get; set; }

        public int TotalPages
        {
            get
            {
                if (TotalCount == 0) return 0;
                return (int) Math.Ceiling((TotalCount*1.0)/(PageSize*1.0));
            }
        }

        /// <summary>
        ///     根据需要具体而定PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     根据需要具体而定 分页显示最大的页数
        /// </summary>
        public int DisplayMaxPages
        {
            get { return 10; }
        }

        public bool IsHasPrePage
        {
            get { return CurrentIndex != 1; }
        }

        public bool IsHasNextPage
        {
            get { return CurrentIndex != TotalPages; }
        }
    }
}