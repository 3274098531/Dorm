using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.Domain.状态
{
    public abstract class State<T>where T:Entity
    {
        public abstract void Handle(T t);
        public abstract string GetMessage();
    }
}
